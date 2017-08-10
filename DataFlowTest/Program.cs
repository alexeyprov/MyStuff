using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DataFlowTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
        }

        private static ITargetBlock<Order> BuildPipeline()
        {
            BufferBlock<Order> orderBlock = new BufferBlock<Order>();

            BroadcastBlock<Order> broadcastBlock = new BroadcastBlock<Order>(o => o);
            orderBlock.LinkTo(broadcastBlock);
            orderBlock.Completion.ContinueWith(t => broadcastBlock.Complete());

            BatchBlock<Order> upsBatchBlock = new BatchBlock<Order>(5);
            broadcastBlock.LinkTo(upsBatchBlock, o => o.Carrier == "UPS");

            BatchBlock<Order> fedexBatchBlock = new BatchBlock<Order>(5);
            broadcastBlock.LinkTo(fedexBatchBlock, o => o.Carrier == "FedEx");
            broadcastBlock.Completion.ContinueWith(t =>
                {
                    upsBatchBlock.Complete();
                    fedexBatchBlock.Complete();
                });

            TransformManyBlock<Order[], ItemTrackingDetail> upsProcessor = 
                new TransformManyBlock<Order[], ItemTrackingDetail>(
                    orders => PostOrdersAsync(orders));
            upsBatchBlock.LinkTo(upsProcessor);
            upsBatchBlock.Completion.ContinueWith(t => upsProcessor.Complete());

            TransformManyBlock<Order[], ItemTrackingDetail> fedexProcessor =
                new TransformManyBlock<Order[], ItemTrackingDetail>(
                    orders => PostOrdersAsync(orders));
            fedexBatchBlock.LinkTo(fedexProcessor);
            fedexBatchBlock.Completion.ContinueWith(t => fedexProcessor.Complete());

            ActionBlock<ItemTrackingDetail> persister = new ActionBlock<ItemTrackingDetail>(
                d => PersistToDatabase(d),
                new ExecutionDataflowBlockOptions
                {
                    MaxDegreeOfParallelism = 3
                });
            upsProcessor.LinkTo(persister);
            fedexProcessor.LinkTo(persister);

            Action<Task> persisterCompletion = t =>
                {
                    Task.WaitAll(upsProcessor.Completion, fedexProcessor.Completion);
                    persister.Complete();
                };
            upsProcessor.Completion.ContinueWith(persisterCompletion);
            fedexProcessor.Completion.ContinueWith(persisterCompletion);

            return orderBlock;
        }

        private static async Task<IEnumerable<ItemTrackingDetail>> PostOrdersAsync(Order[] orders)
        {
            if (orders == null || orders.Length == 0)
            {
                throw new ArgumentException("orders");
            }

            string carrier = orders[0].Carrier;
            IEnumerable<ShippingDetail> shippingDetails = CreateShippingData(orders);

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("http://localhost:9099/submit/" + carrier, shippingDetails, new JsonMediaTypeFormatter());

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("HTTP call failed");
            }

            return await response.Content.ReadAsAsync<IEnumerable<ItemTrackingDetail>>();
        }

        private static async Task PersistToDatabase(ItemTrackingDetail trackingDetail)
        {
            await Task.Delay(50);
        }

        private static IEnumerable<ShippingDetail> CreateShippingData(IEnumerable<Order> orders)
        {
            return orders
                .Select(o => new ShippingDetail
                    {
                        ShipId = o.OrderId,
                        Items = o.Items
                            .Select(i => new ShippingItem
                                {
                                    Sku = i.Sku
                                })
                            .ToArray()
                    })
                .ToArray();
        }
    }
}
