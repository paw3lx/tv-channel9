using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace tvChannel9.Data
{
    public class RssReader
    {
        public static async Task<List<RssVideo>> ParseVideoRss(string uri)
        {
            return await Task.Run(() =>
            {
                XNamespace ns = XNamespace.Get("http://search.yahoo.com/mrss/");
                var xdoc = XDocument.Load(uri);
                var id = 0;
                return (
                    from item in xdoc.Descendants("item")
                    select new RssVideo()
                    {
                    Name = (string)item.Element("title"),
                    Description = (string)item.Element("description"),
                    VideoUrl = (string)item.Element(ns + "group").Elements(ns + "content")
                                           .Where(s => s.Attribute("url").Value.Contains("mid.mp4"))
                                           .FirstOrDefault()?.Attribute("url").Value,
                    ThumbnailUrl = (string) item.Elements(ns + "thumbnail")
                                                .Where(s => int.Parse(s.Attribute("width").Value) > 500)
                                                .FirstOrDefault()?.Attribute("url").Value,
                    Id = id++
                        }).ToList();
            });
        }

    }
}
