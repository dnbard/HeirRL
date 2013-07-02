using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework.Graphics;

namespace HeirRL.Source
{
    class ImagesManager
    {
        private static object syncRoot = new Object();

        private static ImagesManager _instance;
        private static ImagesManager instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new ImagesManager();
                    }
                }

                return _instance;
            }
        }

        Dictionary<string, Image> images = new Dictionary<string, Image>();
        string[] ImageTypes = { ".jpg", ".png" };

        private ImagesManager()
        {
            //images now loaded on demand
            //at startup there is no images in dictionary
            //LoadImages();
        }

        public static Image Get(string name)
        {
            var self = instance;

            if (self.images.ContainsKey(name))
                return self.images[name];
            else if (self.LoadImage(name))
                    return self.images[name];
            return null;
        }

        private void LoadImages()
        {
            const string path = @"Content/images";
            string[] directories = Directory.GetDirectories(path);
            foreach (string directory in directories)
                extractImages(directory);
            extractImages(path);
        }

        private bool LoadImage(string query)
        {
            const string path = @"Content/images";
            string[] directories = Directory.GetDirectories(path);

            string fileName = Regex.Replace(query, @"-", @"/");

            //foreach (string directory in directories)
            //{
                foreach (var imageType in ImageTypes)
                {
                    string fullPath = string.Format("{0}/{1}{2}", path, fileName, imageType);   
                    if (File.Exists(fullPath))
                    {
                        FileStream stream = new FileStream(fullPath, FileMode.Open);
                        Image texture = Image.FromStream(Program.Game.GraphicsDevice, stream, query);
                        if (!images.ContainsKey(query))
                            images.Add(query, texture);
                        stream.Close();

                        return true;
                    }
                }
            //}

            return false;
        }
        
        private void extractImages(string path)
        {
            const string _path = @"Content/images";

            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                string key = Regex.Replace(file, string.Format(@"{0}\\", _path), String.Empty);
                bool isImage = false;
                foreach (string imageType in ImageTypes)
                    if (Regex.IsMatch(key, imageType))
                    {
                        isImage = true;
                        key = Regex.Replace(key, imageType, String.Empty);
                    }

                if (!isImage) continue;

                FileStream stream = new FileStream(file, FileMode.Open);
                key = Regex.Replace(key, @"\\", @"-");
                key = key.ToLower();

                Image texture = Image.FromStream(Program.Game.GraphicsDevice, stream, key);
                if (!images.ContainsKey(key))
                    images.Add(key, texture);
                stream.Close();
            }
        }
    }
}
