using System;
using CSTester;
using CSLogging;
using BehaviorLibrary;
using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Composites;
using BehaviorLibrary.Components.Actions;

namespace Tests
{
	[TestCase]
	public class Issue2
	{
		public Issue2 ()
		{
		}

		CSLogger _log = CSLogger.Instance;

		[BuildUp]
		public void buildup(){
			_log.setEnableLogging (true);
			_log.setEnableDebug (true);
			_log.setEnableError (true);
			_log.setEnableMessage (true);
			_log.loadLog ("./", "issue2.log");
			_log.enterScope ("buildup");
			_log.logMessage ("---------- BEGIN TESTING ISSUE 2 ----------");
			_log.exitScope ();
		}

		[TearDown]
		public void teardown(){
			_log.enterScope ("teardown");
			_log.logMessage ("---------- END TESTING ISSUE 2 ----------");
			_log.exitScope ();
			_log.closeLog ();
		}

		[Test]
		public void test1(){
			_log.enterScope ("test1");

			var foo = new Sequence (new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}));

			Verify.VerifyEquals ("all running is running", true, foo.Behave(), BehaviorReturnCode.Running);

			foo = new Sequence (new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Running;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Success;
			}));

			Verify.VerifyEquals ("all but one running is running", true, foo.Behave(), BehaviorReturnCode.Running);

			foo = new Sequence (new BehaviorAction (delegate() {
				return BehaviorReturnCode.Success;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Success;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Success;
			}), new BehaviorAction (delegate() {
				return BehaviorReturnCode.Success;
			}));

			Verify.VerifyEquals ("all success is success", true, foo.Behave(), BehaviorReturnCode.Success);


			_log.exitScope ();
		}
	}
}

