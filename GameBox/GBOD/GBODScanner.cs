using System;
using System.Collections.Generic;
using GameBox.Utils.Log;

namespace GameBox
{
	namespace GBOD
	{
		internal class GBODScanner
		{

			private GBODStream reader = null;
			private LogClient log = LogManager.getNewLogClient(typeof(GBODScanner).ToString());

			internal void Scan(GBODStream reader_)
			{
				reader = reader_;

				reader.Reset();
			}

			private GBODToken getNextToken(out bool eof,out int error)
			{
				// Move forward to the start of the next token.
				GBODChar ch;
				log.debug("GetNextToken:");
				GBODToken token;
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
					GBODChar first = (GBODChar)ch.Clone();
					string str = "" + first.Ch;

					log.debug("Entering in string mode with first = " + first.ToString());

					do
					{
						ch = reader.getNext();
						str += ch.Ch;
					} while (ch.Ch != '"' && !reader.EOF);
					GBODChar last = (GBODChar)ch.Clone();

					if (reader.EOF)
					{
						eof = true;
						error = 1;
						return new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.ErrorEOFInMiddle, str);;
					}
					log.debug ("Finished reading string on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.ObjectValue, str);
				} else if (ch.IsAlpha)
				{
					GBODChar first = (GBODChar)ch.Clone();
					string str = "" + first.Ch;

					log.debug("Entering in PropertyName");
					do
					{
						ch = reader.getNext();
						str += ch.Ch;
					} while (ch.IsAlphaNumeric && !reader.EOF);
					GBODChar last = (GBODChar)ch.Clone();

					if (reader.EOF)
					{
						eof = true;
						error = 1;
						return new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.ErrorEOFInMiddle, str);;
					}

					log.debug ("Finished reading Property name on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.ObjectName, str);
				} else if (ch.Ch == '{')
				{
					GBODChar first = (GBODChar)ch.Clone();
					string str = "" + first.Ch;
					log.debug("Entering in {");
					GBODChar last = (GBODChar)ch.Clone();
					log.debug ("Finished reading { on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.OpenBracket, str);
				} else if (ch.Ch == '}')
				{
					GBODChar first = (GBODChar)ch.Clone();
					string str = "" + first.Ch;
					log.debug("Entering in }");
					GBODChar last = (GBODChar)ch.Clone();
					log.debug ("Finished reading } on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.CloseBracket, str);
				} else if (ch.Ch == ':')
				{
					GBODChar first = (GBODChar)ch.Clone();
					string str = "" + first.Ch;
					log.debug("Entering in :");
					GBODChar last = (GBODChar)ch.Clone();
					log.debug ("Finished reading : on = " + last.ToString());
					log.debug ("Value: "+str);
					token = new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.Colon, str);
				} else
				{
					GBODChar first = (GBODChar)ch.Clone();
					GBODChar last = (GBODChar)ch.Clone();
					log.debug("Invalid value found on = " + first.ToString());
					eof = false;
					return new GBODToken(first, last, GameBox.GBOD.GBODToken.TokenType.ErrorInvalidChar, ch.Ch.ToString());
				}
				eof = reader.EOF;
				return token;
			}
		}
	}
}

