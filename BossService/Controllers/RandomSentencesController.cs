using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BossService.Models;
using System.IO;

namespace BossService.Controllers
{
	public class RandomSentencesController : ApiController
	{
		// GET: api/RandomSentences
		public GenericRestResponse Get()
		{
			try
			{
				Random r = new Random();
				var currentString = "";
				var returnString = "";
				var returnTitle = "";
				int rInt = 0;
				int tInt = r.Next(1, 3);
				string line;
				var done = false;
				var startedWriting = false;
				var count = 0;
				var pCount = 0;

                int hInt = r.Next(1, 4);
				switch (hInt)
				{
					case 1:
						rInt = r.Next(0, 4129);
						currentString = Resources.Fellowship;
						returnTitle = "The Fellowship of the Ring";
						break;
					case 2:
						rInt = r.Next(0, 3170);
						currentString = Resources.TwoTowers;
						returnTitle = "The Two Towers";
						break;
					case 3:
						rInt = r.Next(0, 2558);
						currentString = Resources.ReturnOfTheKing;
						returnTitle = "The Return of the King";
						break;
				}

                var validStart = false;
                StringReader strReader = new StringReader(currentString);
				while ((line = strReader.ReadLine()) != null)
				{
					if(startedWriting && validStart)
					{
						var charArr = line.ToCharArray();

						for (int idx = 0; idx < charArr.Length; idx++)
						{
							if ((charArr[idx] == '.' || charArr[idx] == '!' || charArr[idx] == '?') && (idx >= 2 && (charArr[idx - 2] != 'M' && charArr[idx - 1] != 'r')))
							{
								pCount++;
								if (pCount < tInt)
								{
									returnString += charArr[idx];
								}
								else
								{
									done = true;
									returnString += charArr[idx];
									break;
								}
							}
							else
							{
								returnString += charArr[idx];
							}
						}
					}
					if (count == rInt)
					{
						if(line != "")
						{
							startedWriting = true;
							var charArr = line.ToCharArray();

                            for (int idx=0;idx<charArr.Length;idx++)
							{
								if(!validStart && (char.IsUpper(charArr[idx]) || charArr[idx] == '.' || charArr[idx] == '!' || charArr[idx] == '?'))
								{
									validStart = true;
                                }
								if(validStart)
								{
									if ((charArr[idx] == '.'|| charArr[idx] == '!'|| charArr[idx] == '?') && (idx >= 2 &&(charArr[idx-2] != 'M' && charArr[idx - 1] != 'r')))
									{
										pCount++;
										if (pCount < tInt)
										{
											returnString += charArr[idx];
										}
										else
										{
											done = true;
											returnString += charArr[idx];
											break;
										}
									}
									else
									{
										returnString += charArr[idx];
									}
								}
							}
						}
						else
						{
							rInt++;
						}
					}
					count++;
                    if(startedWriting && !validStart)
                    {
                        rInt++;
                    }
					if(done)
					{
						break;
					}
				}
                var charArr2 = returnString.ToCharArray();
                if(charArr2[0] == '.')
                {
                    returnString = returnString.Substring(2, returnString.Length-2);
                }
				return new GenericRestResponse
				{
					Success = true,
					Response = new RandomSentenceReply
					{
						BookTitle = returnTitle,
						Sentences = returnString
					}
				};
			}
			catch(Exception e)
			{
				return new GenericRestResponse
				{
					Success = false,
					Response = new GenericRestError
					{
						Exception = e,
						HumanMessage = "Something went wrong...Please try again."
					}
				};
			}
		}

		// GET: api/RandomSentences/5
		public string Get(int id)
		{
			return "That method is not currently supported, sorry. Take the id off your url for a random sentence.";
		}

		// POST: api/RandomSentences
		public void Post([FromBody]string value)
		{
		}

		// PUT: api/RandomSentences/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RandomSentences/5
		public void Delete(int id)
		{
		}
	}
}
