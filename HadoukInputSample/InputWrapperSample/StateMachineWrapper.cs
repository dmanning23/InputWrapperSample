using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InputWrapperSample
{
	/// <summary>
	/// This is a crappy fake state machine for mapping move names to state message
	/// in reality, you would have something that actually did something
	/// </summary>
	class StateMachineWrapper
	{
		public List<string> MoveNames { get; private set; }

		public StateMachineWrapper()
		{
			MoveNames = new List<string>();
		}

		public int NameToIndex(string strMessageName)
		{
			MoveNames.Add(strMessageName);
			return MoveNames.Count - 1;
		}
	}
}
