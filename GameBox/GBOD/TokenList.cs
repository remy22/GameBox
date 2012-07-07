using System;
using System.Collections.Generic;

namespace GameBox
{
	namespace GBOD
	{
		internal class TokenList
		{
			private List<Token> tList = new List<Token>();
			private bool error = false;

			internal TokenList ()
			{
			}

			internal void AddToken(Token token)
			{
				tList.Add(token);
				if (token.Error)
					error = true;
			}

			public override string ToString ()
			{
				string result = "";
				foreach (Token t in tList)
				{
					result += t.ToString() + ", ";
				}

				return result;
			}
		}
	}
}

