package main

import "fmt"

func mergeKLists(lists []*ListNode) *ListNode {
	n := len(lists)
	if n == 0 {
		return nil
	} else if n == 1 {
		return lists[0]
	}

	// build min heap
	// HACK: do it in-place
	heap := lists //make([]*ListNode, n)

	siftDown := func(size int, index int) {
		for swapped := true; swapped; {
			least := index

			left := (index << 1) + 1
			if left < size && heap[left].Val < heap[index].Val {
				least = left
			}

			right := left + 1
			if right < size && heap[right].Val < heap[least].Val {
				least = right
			}

			if least == index {
				swapped = false
			} else {
				tmp := heap[index]
				heap[index] = heap[least]
				heap[least] = tmp
				index = least
			}
		}
	}

	guard := new(ListNode)
	lastParent := (n >> 1) - 1
	for i := n - 1; i >= 0; i-- {
		if heap[i] == nil {
			heap[i] = guard
		}

		if i <= lastParent {
			siftDown(n, i)
		}
	}

	// process heap
	var result *ListNode = nil
	var parent *ListNode = nil
	for size := n; size > 0; {
		top := heap[0]
		if top.Next != nil {
			heap[0] = top.Next
		} else {
			heap[0] = heap[size-1]
			size--
		}

		// sift down
		siftDown(size, 0)

		if top == guard {
			continue
		}

		if parent == nil {
			result = top
		} else {
			parent.Next = top
		}

		parent = top
	}

	return result
}

func runTest(lists [][]int, expected ...int) {
	input := make([]*ListNode, len(lists))
	fmt.Print("[")
	first := true
	for j, l := range lists {
		var child *ListNode = nil
		for i := len(l) - 1; i >= 0; i-- {
			n := new(ListNode)
			n.Val = l[i]
			n.Next = child

			child = n
		}

		input[j] = child
		if !first {
			fmt.Print(", ")
		}

		fmt.Print(l)
		first = false
	}

	var actual *ListNode = mergeKLists(input)
	fmt.Print("] => [")
	first = true

	if expected == nil {
		for node := actual; node != nil; node = node.Next {
			if first {
				fmt.Print(node.Val)
			} else {
				fmt.Printf(", %d", node.Val)
			}

			first = false
		}
	} else {
		node := actual
		for _, v := range expected {
			if node == nil {
				panic("premature end of result")
			}

			if v != node.Val {
				err := fmt.Sprintf("Expected %d, got %d", v, node.Val)
				panic(err)
			}

			if first {
				fmt.Print(v)
			} else {
				fmt.Printf(", %d", v)
			}

			node = node.Next
			first = false
		}
	}

	fmt.Print("]\n")
}

func main() {
	// original #1
	runTest([][]int{{1, 4, 5}, {1, 3, 4}, {2, 6}}, 1, 1, 2, 3, 4, 4, 5, 6)

	// original #2
	runTest([][]int{})

	// original #3
	runTest([][]int{nil})

	// own #1
	runTest([][]int{{1, 1}, {1}, {1, 1, 1}}, 1, 1, 1, 1, 1, 1)

	// own #2
	runTest([][]int{{2, 9}, {1}, {}, {4, 6, 7}}, 1, 2, 4, 6, 7, 9)
}
