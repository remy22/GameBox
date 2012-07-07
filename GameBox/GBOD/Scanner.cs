using System;
using System.Collections.Generic;
using GameBox.Utils.Log;

namespace GameBox
{
	namespace GBOD
	{
		internal class Scanner
		{

			private Stream reader = null;
			private LogClient log = LogManager.getNewLogClient(typeof(Scanner).ToString());

			internal void Scan(Stream reader_)
			{
				reader = reader_;

				reader.Reset();
			}

			private Token getNextToken(out bool eof,out int error)
			{
				// Move forward to the start of the next token.
				Character ch;
				log.debug("GetNextToken:");
				Token token;
				error = 0;

				do
				{
					ch = reader.getNext();
				} while (ch.Ch == ' ' || ch.Ch == '\n' || ch.Ch == '\r' || ch.Ch == '\t' || !reader.EOF);

				if (reader.EOF)
				{
					eof = true;
					return null;
				}

				if (ch.Ch == '"')
				{
					Character first = (Character)ch.Clone();
					string str = "" + first.Ch;

					log.debug("Entering in string mode with first = " + first.ToString());

					do
					{
						ch = reader.getNext();
						str += ch.Ch;
					} while (ch.Ch != '"' && !reader.EOF);
					Character last = (Character)ch.Clone();

					if (reader.EOF)
					{
						eof = true;
						error = 1;
						return new Token(first, last, GameBox.GBOD.Token.TokenType.ErrorEOFInMiddle, str);;
					}
					log.debug ("Finished reading string on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new Token(first, last, GameBox.GBOD.Token.TokenType.ObjectValue, str);
				} else if (ch.IsAlpha)
				{
					Character first = (Character)ch.Clone();
					string str = "" + first.Ch;

					log.debug("Entering in PropertyName");
					do
					{
						ch = reader.getNext();
						str += ch.Ch;
					} while (ch.IsAlphaNumeric && !reader.EOF);
					Character last = (Character)ch.Clone();

					if (reader.EOF)
					{
						eof = true;
						error = 1;
						return new Token(first, last, GameBox.GBOD.Token.TokenType.ErrorEOFInMiddle, str);;
					}

					log.debug ("Finished reading Property name on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new Token(first, last, GameBox.GBOD.Token.TokenType.ObjectName, str);
				} else if (ch.Ch == '{')
				{
					Character first = (Character)ch.Clone();
					string str = "" + first.Ch;
					log.debug("Entering in {");
					Character last = (Character)ch.Clone();
					log.debug ("Finished reading { on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new Token(first, last, GameBox.GBOD.Token.TokenType.OpenBracket, str);
				} else if (ch.Ch == '}')
				{
					Character first = (Character)ch.Clone();
					string str = "" + first.Ch;
					log.debug("Entering in }");
					Character last = (Character)ch.Clone();
					log.debug ("Finished reading } on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new Token(first, last, GameBox.GBOD.Token.TokenType.CloseBracket, str);
				} else if (ch.Ch == ':')
				{
					Character first = (Character)ch.Clone();
					string str = "" + first.Ch;
					log.debug("Entering in :");
					Character last = (Character)ch.Clone();
					log.debug ("Finished reading : on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new Token(first, last, GameBox.GBOD.Token.TokenType.Colon, str);
				} else
				{
					Character first = (Character)ch.Clone();
					Character last = (Character)ch.Clone();
					log.debug("Invalid value found on = " + first.ToString());
					eof = false;
					return new Token(first, last, GameBox.GBOD.Token.TokenType.ErrorInvalidChar, ch.Ch.ToString());
				}
				eof = reader.EOF;
				return token;
			}
		}
	}
}

