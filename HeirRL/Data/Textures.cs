using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using HeirRL.Source;
using Microsoft.Xna.Framework;

namespace HeirRL.Data
{
    public class Textures
    {
        public List<RectangleName> GetTextureRectangles(string name)
        {
            var result = new List<RectangleName>();

            var strings = Regex.Split(name, @"-");
            string path = strings[0];
            string image = strings[1];

            using (var connection = (SQLiteConnection)SQLiteConnector.Factory.CreateConnection())
            {
                connection.ConnectionString = SQLiteConnector.ConnectionString;
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = string.Format("SELECT * FROM textures WHERE path = '{0}' AND image = '{1}'", path, image);
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        var SubTextureName = reader["texture"].ToString();
                        int left = (int)reader["left"],
                            top = (int)reader["top"],
                            width = (int)reader["width"],
                            height = (int)reader["height"];

                        bool isAnimation = (bool)reader["animation"];
                        if (!isAnimation)
                        {
                            var rect = new RectangleName();
                            rect.Rect = new Rectangle(left, top, width, height);
                            rect.Name = SubTextureName;
                            result.Add(rect);
                        }
                        else
                        {
                            int horframes = (int)reader["hframes"];
                            int vertframes = (int)reader["vframes"];

                            int maxFrames = (int)reader["maxframes"];
                            if (maxFrames == 0) maxFrames = horframes * vertframes;

                            int counter = 0;
                            for (int j = 0; j < vertframes; j++)
                            {
                                for (int i = 0; i < horframes; i++)
                                {
                                    string texturename = string.Format("{0}{1}", SubTextureName, counter);
                                    result.Add(new RectangleName()
                                    {
                                        Rect = new Rectangle(left + width * i, top + height * j, width, height),
                                        Name = texturename
                                    });
                                    counter++;
                                    if (counter == maxFrames) break;
                                }
                                if (counter == maxFrames) break;
                            }
                        }
                    }
                    
                }
                connection.Close();
            }

            return result;
        }
    }
}
