using System;
using CSTester;
using CSLogging;
using BehaviorLibrary;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Actions;

namespace Tests
{
	[TestCase]
	public class Issue7
	{
		public Issue7 ()
		{
		}

		private CSLogger _log = CSLogger.Instance;

		[BuildUp]
		public void build_up(){
			_log.setEnableLogging (true);
			_log.setEnableDebug (true);
			_log.setEnableError (true);
			_log.setEnableMessage (true);
			_log.loadLog ("./", "issue7.log");
			_log.enterScope ();
			_log.logMessage ("---------- BEGIN TESTING ISSUE 7 ----------");
			_log.exitScope ();

		}

		[TearDown]
		public void tear_down(){
			_log.enterScope ();
			_log.logMessage ("---------- END TESTING ISSUE 7 ----------");
			_log.exitScope ();
			_log.closeLog ();
		}

		private int[] counts = new int[4];

		public BehaviorReturnCode component_1(){
			counts [0]++;

			return BehaviorReturnCode.Success;
		}

		public BehaviorReturnCode component_2(){
			counts [1]++;
			return BehaviorReturnCode.Success;
		}

		public BehaviorReturnCode component_3(){
			counts [2]++;
			return BehaviorReturnCode.Success;
		}

		public BehaviorReturnCode component_4(){
			counts [3]++;
			return BehaviorReturnCode.Success;
		}


		[Test]
		public void test_1(){
			_log.enterScope ();

			RandomSelector rs = new RandomSelector (new BehaviorAction(component_1),
				new BehaviorAction(component_2),
				new BehaviorAction(component_3),
				new BehaviorAction(component_4));


			for (int i = 0; i < 100000; i++)
				rs.Behave ();

			_log.logMessage ("1:" + counts[0] +", 2:" + counts[1]+ ", 3:" + counts[2]+ ", 4:" + counts [3]);

			Verify.VerifyTrue ("verify last component actioned", true, counts [3] > 0);

			_log.exitScope ();
		}
	}
}

