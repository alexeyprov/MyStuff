using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;

namespace FileWatcher
{
	public class FileWatcher : System.ServiceProcess.ServiceBase
	{
		private System.IO.FileSystemWatcher fsw;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FileWatcher()
		{
			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitComponent call
		}

		// The main entry point for the process
		static void Main()
		{
			System.ServiceProcess.ServiceBase[] ServicesToRun;
	
			// More than one user Service may run within the same process. To add
			// another service to this process, change the following line to
			// create a second service object. For example,
			//
			//   ServicesToRun = new System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
			//
			ServicesToRun = new System.ServiceProcess.ServiceBase[] { new FileWatcher() };

			System.ServiceProcess.ServiceBase.Run(ServicesToRun);
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.fsw = new System.IO.FileSystemWatcher();
			((System.ComponentModel.ISupportInitialize)(this.fsw)).BeginInit();
			// 
			// fsw
			// 
			this.fsw.EnableRaisingEvents = true;
			this.fsw.Path = "C:\\";
			this.fsw.Deleted += new System.IO.FileSystemEventHandler(this.fsw_Deleted);
			this.fsw.Renamed += new System.IO.RenamedEventHandler(this.fsw_Renamed);
			this.fsw.Changed += new System.IO.FileSystemEventHandler(this.fsw_Changed);
			this.fsw.Created += new System.IO.FileSystemEventHandler(this.fsw_Created);
			// 
			// FileWatcher
			// 
			this.CanHandlePowerEvent = true;
			this.CanPauseAndContinue = true;
			this.CanShutdown = true;
			this.ServiceName = "FileWatcher";
			((System.ComponentModel.ISupportInitialize)(this.fsw)).EndInit();

		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			if (args.Length > 0)
			{
				fsw.Path = args[0];
			}
			EventLog.WriteEntry(
		        String.Format("FileWatchService starting. " +
			        "There are {0} args. Watch path is '{1}'", 
					args.Length, fsw.Path));

		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			// TODO: Add code here to perform any tear-down necessary to stop your service.
		}

		private void fsw_Changed(object sender, System.IO.FileSystemEventArgs e)
		{
			EventLog.WriteEntry( 
				String.Format("File/folder changed: {0}." +
				"Change type: {1}. ", 
				e.Name, e.ChangeType));	
		}

		private void fsw_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			EventLog.WriteEntry( 
				String.Format(
				"File/folder created: {0}.", e.FullPath), 
				EventLogEntryType.Information);		
		}

		private void fsw_Deleted(object sender, System.IO.FileSystemEventArgs e)
		{
			EventLog.WriteEntry(
				String.Format(
				"File/folder deleted: {0}.", e.FullPath));		
		}

		private void fsw_Renamed(object sender, System.IO.RenamedEventArgs e)
		{
			EventLog.WriteEntry(
				String.Format("File/folder renamed " + 
				"from {0} to {1}.", 
				new object[] {e.OldFullPath, e.FullPath}), 
				EventLogEntryType.Information);		
		}
	}
}
