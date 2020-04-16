﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;

namespace KosenMobile.src.rss{
  class DataManager{
    HttpClient client_;
    Context context_;
    public DataManager(Context _context){
      context_ = _context;
      if (!File.Exists(System.IO.Path.Combine(context_.DataDir.Path, "rss.db"))) {
        CreateNew();
        Log.Debug("RSS.DataManager", "Create New Database");
      }

        Load();
        Log.Debug("RSS.DataManager", "Load Database");
    }

   async Task CreateNew()
    {
     await Task.Run(async () =>
      {
        client_ = new HttpClient();
        using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri("https://me.milkcocoa.info/rss.db")))
        using (var response = await client_.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
        {

          if (response.StatusCode == HttpStatusCode.OK)
          {

            var content = response.Content;
            var stream = await content.ReadAsStreamAsync();
            var path = System.IO.Path.Combine(context_.DataDir.Path, "rss.db");
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);


            stream.CopyTo(fileStream);
          }
        }
      });

    }

    void Update()
    {
      if (!CheckForUpdate()) return;
    }

    bool CheckForUpdate()
    {
      return false;
    }

    async Task Load()
    {
      await Task.Run(() => {
        System.Threading.Thread.Sleep(5000);
      });
    }

  }
}