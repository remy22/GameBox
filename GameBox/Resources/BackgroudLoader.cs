using System;
using System.Collections.Generic;
using System.IO;
using GameBox.Services;
using System.Threading;
using GameBox.Graphics.Nodes;

namespace GameBox.Resources
{
    internal static class BackgroudLoader
    {
        private static Dictionary<string,string> filesToLoad;
        private static long counterTotal;
        private static long currentCounter;
        private static Dictionary<string,Resource> readingResource;
        private static Resource currentResource = null;
        private static bool working = false;
        private static bool finished = false;

        internal static void LoadResourceList(Dictionary<string, string> fileList)
        {
            finished = false;
            working = false;
            filesToLoad = fileList;
            Thread th = new Thread(AsyncLoadResource);
            th.Start();
        }

        private static void AsyncLoadResource()
        {
            readingResource = new Dictionary<string, Resource>();
            counterTotal = 0;
            currentCounter = 0;

            foreach (KeyValuePair<string, string> kv in filesToLoad)
            {
                Logger.debug("Adding file: " + kv.Value);
                Resource r = new Resource(kv.Key, kv.Value);
                counterTotal += r.Info.Length;
                Logger.debug("With len:" + r.Info.Length);
                readingResource.Add(kv.Key, r);
            }

            working = true;
            foreach (KeyValuePair<string, Resource> kv in readingResource)
            {
                lock (currentResource) { currentResource = kv.Value; }
                kv.Value.ReadResource();
                currentCounter += kv.Value.BytesRead;
            }
            working = false;
            lock (currentResource) { currentResource = null; }
            finished = true;
		}

        internal static LoaderControllerScene CreateLoaderScene()
        {
            return null;
        }

		internal static long TotalRead
		{
            get
            {
                long temp = currentCounter;
                lock (currentResource)
                {
                    if (currentResource != null)
                        temp += currentResource.BytesRead;
                }
                return temp;
            }
		}
    }
}
