﻿using System;
using System;

using Android.OS;
using Android.Widget;
using Android.Content;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using Android.Runtime;
using Android.App;
using Android.Graphics;


namespace KosenMobile.rss {
  public class Adapter : RecyclerView.Adapter {
    IReadOnlyList<DataModel.Model> model_;
    List<DataModel.Model> rows_;
    Activity activity_;
    public Adapter(Activity _activity, IReadOnlyList<DataModel.Model> _model) {
      activity_ = _activity;
      model_ = _model;
    }

    public Adapter(Activity _activity) {
      activity_ = _activity;
    }

    public override int ItemCount => model_.Count;
    public Action<DataModel.Model> onRowClicked;

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
      ((Holder)(holder)).created_.Text = DateTime.Parse(model_[position].date_).ToString("yyyy-MM-dd");
      ((Holder)(holder)).content_.Text = model_[position].title_;
      ((Holder)(holder)).id_ = model_[position].id_;
      ((Holder)(holder)).hash_ = model_[position].hash_;
      ((Holder)(holder)).detail_ = model_[position].detail_;
      ((Holder)(holder)).content_.Click += async (sender, e) => {
        if(!((TextView)sender).Clickable) return;
        ((TextView)sender).Clickable = false;

        new Handler().PostDelayed(async () => {
          ((TextView)sender).Clickable = true;
        }, 1000);


        if(((Holder)holder).detail_.Contains(".pdf")) {
          var intent = new Android.Content.Intent(Intent.ActionView);
          intent.SetDataAndType(Android.Net.Uri.Parse(((Holder)holder).detail_), "application/pdf");
          activity_.StartActivity(intent);

        } else {
          var intent = new Android.Content.Intent(activity_.ApplicationContext, typeof(rss.RSSDetailActivity));
          intent.PutExtra("detail", ((Holder)holder).detail_);
          activity_.StartActivity(intent);
        }
      };

      if(((Holder)holder).detail_.EndsWith(".pdf")) {
        ((Holder)holder).pdfIcon_.Visibility = ViewStates.Visible;
      } else {
        ((Holder)holder).pdfIcon_.Visibility = ViewStates.Invisible;
      }

    }

    public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
      return new Holder(LayoutInflater
            .From(parent.Context)
            .Inflate(Resource.Layout.rss_summary,
                parent,
                false));
    }
  }
}


