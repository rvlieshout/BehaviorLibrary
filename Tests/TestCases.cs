//
//  TestCases.cs
//
//  Author:
//       Thomas H. Jonell <@Net_Gnome>
//
//  Copyright (c) 2013 Thomas H. Jonell
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using CSTester;
using CSLogging;
using BehaviorLibrary;
using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Actions;

namespace Tests
{
	[TestCase]
	public class TestCases
	{
		public TestCases (){}

		private CSLogger _log = CSLogger.Instance;

		[BuildUp]
		public void buildup(){
			_log.setEnableLogging (true);
			_log.setEnableDebug (true);
			_log.setEnableError (true);
			_log.setEnableMessage (true);
			_log.loadLog("","behaviorLibrary.log");
			_log.enterScope ("TestCases");
			_log.logMessage ("----------------- STARTING BEHAVIOR LIBRARY TESTS -----------------");
		}

		[TearDown]
		public void teardown(){
			_log.enterScope("teardown");
			_log.exitScope ();
			_log.logMessage ("----------------- ENDING BEHAVIOR LIBRARY TESTS -----------------");
			_log.exitScope ();
			_log.closeLog ();
		}

		[Test]
		public void testStatefulSeq(){
			_log.enterScope("testStatefulSeq");

			bool first = true;

			var foo = new StatefulSequence (new BehaviorAction(delegate(){
				if(first){
					return BehaviorReturnCode.Success;
				}else{
					return BehaviorReturnCode.Failure;
				}
			}),new BehaviorAction( delegate(){
				if(first){
					first = false;
					return BehaviorReturnCode.Running;
				}else{
					return BehaviorReturnCode.Success;
				}
			}),new BehaviorAction(delegate(){
				return BehaviorReturnCode.Success;
			}));

			Verify.VerifyEquals ("1st running", true, foo.Behave (), BehaviorReturnCode.Running);
			Verify.VerifyEquals ("2nd success", true, foo.Behave (), BehaviorReturnCode.Success);
			Verify.VerifyEquals ("3rd failure", true, foo.Behave (), BehaviorReturnCode.Failure);

			_log.logMessage ("restting first");
			first = true;

			Verify.VerifyEquals ("after reset running", true, foo.Behave (), BehaviorReturnCode.Running);
			Verify.VerifyEquals ("final success", true, foo.Behave (), BehaviorReturnCode.Success);
			Verify.VerifyEquals ("final failure", true, foo.Behave (), BehaviorReturnCode.Failure);

			_log.exitScope ();
		}

		[Test]
		public void testStatefulSel(){
			_log.enterScope("testStatefulSel");

			bool first = true;
			bool second = true;
			var foo = new StatefulSelector (new BehaviorAction (delegate(){
				return BehaviorReturnCode.Failure;
			}), new BehaviorAction (delegate() {
				if(first){
					first = false;
					return BehaviorReturnCode.Running;
				}else{
					return BehaviorReturnCode.Failure;
				}
			}), new BehaviorAction (delegate(){
				if(first){
					return BehaviorReturnCode.Success;
				}else{
					if(second){
						second = false;
						return BehaviorReturnCode.Success;
					}else{
						return BehaviorReturnCode.Failure;
					}
				}
			}));

			Verify.VerifyEquals ("1st running", true, foo.Behave (), BehaviorReturnCode.Running);
			Verify.VerifyEquals ("2nd success", true, foo.Behave (), BehaviorReturnCode.Success);
			Verify.VerifyEquals ("3rd failure", true, foo.Behave (), BehaviorReturnCode.Failure);

			_log.logMessage ("restting flags");
			first = true;
			second = true;

			Verify.VerifyEquals ("after reset running", true, foo.Behave (), BehaviorReturnCode.Running);
			Verify.VerifyEquals ("final success", true, foo.Behave (), BehaviorReturnCode.Success);
			Verify.VerifyEquals ("final failure", true, foo.Behave (), BehaviorReturnCode.Failure);

			_log.exitScope ();
		}
	}
}

