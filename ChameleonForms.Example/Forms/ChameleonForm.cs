﻿using System;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using ChameleonForms.Example.Forms.Templates;

namespace ChameleonForms.Example.Forms
{
    public class ChameleonForm<TModel, TTemplate> : IDisposable where TTemplate : IFormTemplate, new()
    {
        public HtmlHelper<TModel> HtmlHelper { get; private set; }
        public TTemplate Template { get; private set; }

        public ChameleonForm(HtmlHelper<TModel> helper, string action, HttpMethod method, string enctype)
        {
            HtmlHelper = helper;
            Template = new TTemplate();
            Write(Template.BeginForm(action, method, enctype));
        }

        public void Write(IHtmlString htmlString)
        {
            HtmlHelper.ViewContext.Writer.Write(htmlString);
        }

        public void Dispose()
        {
            Write(Template.EndForm());
        }
    }

    public static class ChameleonFormExtensions
    {
        public static ChameleonForm<TModel, DefaultFormTemplate> BeginChameleonForm<TModel>(this HtmlHelper<TModel> helper, string action, HttpMethod method, string enctype = null)
        {
            return new ChameleonForm<TModel, DefaultFormTemplate>(helper, action, method, enctype);
        }
    }
}