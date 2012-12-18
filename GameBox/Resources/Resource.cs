using System;

namespace GameBox
{
	public abstract class Resource
	{
		public Resource ()
		{
		}

		public bool Load(string fileName)
		{
			if (GBFileSystem.FileExists(fileName))
			{

			}
			return false;
		}

		public abstract bool LoadImplement(string fileName);
	}
}

