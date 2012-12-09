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
			if (FileSystem.FileExists(fileName))
			{

			}
			return false;
		}

		public abstract bool LoadImplement(string fileName);
	}
}

