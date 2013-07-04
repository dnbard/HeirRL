using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HeirRL.Scenes
{
    class SceneManager : DrawableGameComponent
    {
        private static object syncRoot = new Object();

        private static SceneManager _instance;
        private static SceneManager instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new SceneManager();
                    }
                }

                return _instance;
            }
        }

        private Dictionary<string, Scene> scenes;

        public static SceneLevel CurrentLevel { get; protected set; }

        private Scene _current;
        public static Scene Current
        {
            get { return instance._current; }
            set
            {
                instance._current = value;
                if (!instance.scenes.ContainsValue(value))
                    instance.scenes.Add(value.Name,value);

                var level = value as SceneLevel;
                if (level != null) CurrentLevel = level;
            }
        }

        private Scene _modal;
        public static Scene Modal
        {
            get { return instance._modal; }
            set
            {
                if (value is SceneLevel) throw new ArgumentException("Level can't be a modal scene.");

                instance._modal = value;
                if (!instance.scenes.ContainsValue(value))
                    instance.scenes.Add(value.Name, value);                
            }
        }

        public static Scene Get(string name)
        {
            var dictionary = instance.scenes;
            return dictionary.ContainsKey(name) ? dictionary[name] : null;
        }

        public static bool Find(string sceneName)
        {
            return instance.scenes.ContainsKey(sceneName);
        }

        public static void Set(string sceneName)
        {
            var dictionary = instance.scenes;
            if (dictionary.ContainsKey(sceneName))
                Current = dictionary[sceneName];
        }

        public static void Set(Scene sceneObject)
        {
            var Scenes = instance.scenes;
            if (Scenes == null) throw new ArgumentNullException("Scenes");
            if (Scenes.ContainsValue(sceneObject))
                Current = sceneObject;
            else
            {
                Scenes.Add(sceneObject.Name, sceneObject);
                Current = sceneObject;
            }
        }

        public static void Delete(Scene SceneObject)
        {
            var Scenes = instance.scenes;
            if (Scenes.ContainsValue(SceneObject))
            {
                if (Current == SceneObject)
                    Current = null;
                Scenes.Remove(SceneObject.Name);
            }
        }

        public static void Delete(string SceneName)
        {
            var Scenes = instance.scenes;
            if (Scenes.ContainsKey(SceneName))
            {
                if (Current.Name == SceneName)
                    Current = null;
                Scenes.Remove(SceneName);
            }
        }

        public static void Add(Scene SceneObject)
        {
            var Scenes = instance.scenes;
            Scenes.Add(SceneObject.Name, SceneObject);
        }

        private SceneManager():base(Program.Game)
        {
            scenes = new Dictionary<string, Scene>();
        }

        GameTime LastUpdate = new GameTime(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0));
        GameTime LastDraw = new GameTime(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(0));
        public static new void Draw(GameTime gameTime)
        {
            var manager = instance;
            manager.LastDraw = gameTime;
            manager._current.Draw(gameTime);
            if (manager._modal != null)
                manager._modal.Draw(gameTime);
        }

        public static new void Update(GameTime gameTime)
        {
            var manager = instance;
            manager.LastUpdate = gameTime;
            if (manager._modal != null)
                manager._modal.Update(gameTime);
            else manager._current.Update(gameTime);
        }

        public static void RemoveElement(IGameComponent element)
        {
            var sceneCurrent = Current;
            if (!Current.Remove(element))
                if (instance.scenes.Any(pair => pair.Value.Remove(element))) 
                    return;
        }
    }
}
