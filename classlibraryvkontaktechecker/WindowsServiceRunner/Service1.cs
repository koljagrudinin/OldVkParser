using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsServiceRunner
{
	public partial class VKGroupsStatisticAggregator : ServiceBase
	{
		ClassLibraryPlanner.Planner worker;// = new ClassLibraryPlanner.Planner();

		System.Threading.Tasks.Task workerThread;

		public VKGroupsStatisticAggregator( )
		{
			InitializeComponent();
			workerThread = new System.Threading.Tasks.Task( initializeWorker );//

			workerThread.Start();
		}

		void initializeWorker( )
		{
			worker = new ClassLibraryPlanner.Planner();
		}

		protected override void OnStart( string[] args )
		{
			new System.Threading.Thread( workerStart ).Start();
		}
		void workerStart( )
		{
			workerThread.Wait( -1 );
			worker.Start();
		}

		protected override void OnStop( )
		{
			new System.Threading.Thread( worker.Stop ).Start();
		}
	}
}
