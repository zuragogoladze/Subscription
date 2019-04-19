using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace TBC.Capital.Core.Utils
{
    public static class HtmlExtensions
    {
        public static string LsImage(this UrlHelper helper, string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
            {
                return "";
            }
            var request = System.Web.HttpContext.Current.Request;
            var url = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);
            var imgpath = url + imageName;
            return imgpath;
        }

        public static string CKImage(this UrlHelper helper, string imageName)
        {
            //var request = System.Web.HttpContext.Current.Request;
            //var url = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);

            return "/Uploads/ckimages/" + imageName;
        }

        public static string CKImageRemoveUrl(this UrlHelper helper, string imageName)
        {
            var request = System.Web.HttpContext.Current.Request;
            var url = string.Format("{0}://{1}", request.Url.Scheme, request.Url.Authority);

            return imageName.Replace(url, "");
        }

        public static string InputValidation<TModel>(
            this HtmlHelper<TModel> html,
            string propertyName)
        {
            try
            {
                var modelState = html.ViewData.ModelState;

                if (modelState[propertyName] != null)
                {
                    var property = modelState[propertyName];
                    if (property.Errors.Any())
                        return "error";
                }

                throw new Exception();
            }
            catch
            {
                return "";
            }
        }


        public static string InputRegistrationValue<TModel>(
            this HtmlHelper<TModel> html,
            string propertyName)
        {
            try
            {
                var model = html.ViewBag.RegistrationModel;
                return model.GetType().GetProperty(propertyName).GetValue(model, null);
            }
            catch
            {
                return "";
            }
        }


        public static string GetActive(string controllerName, string currentVal, string actionName = "", string currentVal1 = "", string parameter = "", string currentParameter = "")
        {
            if (string.IsNullOrWhiteSpace(actionName) && controllerName.ToLower().Equals(currentVal.ToLower()) && parameter.ToLower().Equals(currentParameter.ToLower()))
                return "m-menu__item--active";

            if (controllerName.ToLower().Equals(currentVal.ToLower()) && actionName.ToLower().Equals(currentVal1.ToLower()) && parameter.ToLower().Equals(currentParameter.ToLower()))
                return "m-menu__item--active";

            return "";
        }

        public static string GetActive(string controllerName, string[] currentVal)
        {
            if (currentVal.Contains(controllerName))
                return "m-menu__item--active m-menu__item--open";

            return "";
        }

        public static MvcHtmlString LsCheckBox<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            bool isEnabled = true,
            bool showLabel = true,
            bool isCheckByDefault = true)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            string propertyName = metaData.PropertyName;
            string displayName = metaData.DisplayName ?? propertyName;
            string disabled = isEnabled ? "" : "disabled";

            var value = metaData.Model?.ToString();
            bool isChecked = false;


            if (value == null)
            {
                if (isCheckByDefault)
                {
                    isChecked = true;
                }
            }
            else
            {
                if (value == "True")
                {
                    isChecked = true;
                }
            }
            var OuterDiv = new TagBuilder("div");
            OuterDiv.AddCssClass("form-group row m-form__group");

            var fieldLabel = new TagBuilder("label");
            if (showLabel)
            {
                fieldLabel.Attributes.Add("for", propertyName);
                fieldLabel.AddCssClass("col-form-label col-md-2");
                fieldLabel.InnerHtml = displayName;
                OuterDiv.InnerHtml = fieldLabel.ToString();
            }



            bool hasError = false;

            var validationSpan = new TagBuilder("span");
            validationSpan.AddCssClass("form-control-feedback");

            var modelState = html.ViewData.ModelState;

            if (modelState[propertyName] != null)
            {
                var error = modelState[propertyName].Errors.FirstOrDefault();
                if (error != null)
                {
                    hasError = true;
                    validationSpan.InnerHtml = error.ErrorMessage;
                }
            }

            if (hasError)
            {
                OuterDiv.AddCssClass("has-danger");
            }

            var inputWrapper = new System.Web.Mvc.TagBuilder("div");
            inputWrapper.AddCssClass("col-md-8");

            var inputWrapperChild = new TagBuilder("div");
            inputWrapperChild.AddCssClass("checkbox m-switch");

            var inputWrapperSpan = new TagBuilder("span");
            inputWrapperSpan.AddCssClass("m-switch m-switch--icon");

            var inputWrapperLabel = new TagBuilder("label");

            var input = new TagBuilder("input");
            input.Attributes.Add("id", propertyName);
            input.Attributes.Add("name", propertyName);

            input.Attributes.Add("type", "checkbox");

            input.Attributes.Add("value", "true");
            if (!isEnabled)
            {
                input.Attributes.Add("disabled", "true");
            }

            if (isChecked)
            {
                input.Attributes.Add("checked", null);
            }

            inputWrapperLabel.InnerHtml = input.ToString() + "<input name='" + propertyName + "' type='hidden' value='false' />" + " <span></span>";
            inputWrapperSpan.InnerHtml = inputWrapperLabel.ToString();
            inputWrapperChild.InnerHtml = inputWrapperSpan.ToString() + validationSpan.ToString();
            inputWrapper.InnerHtml = inputWrapperChild.ToString();

            OuterDiv.InnerHtml = fieldLabel.ToString() + inputWrapper.ToString();

            return MvcHtmlString.Create(OuterDiv.ToString());
        }

        public static MvcHtmlString LsEditor<TModel, TValue>(
          this HtmlHelper<TModel> html,
          Expression<Func<TModel, TValue>> expression,
          bool isThisEditor = true,
          string LabelAddText = "",
          object htmlAttributes = null)
        {
            var rvd = new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var memberExpression = (MemberExpression)expression.Body;
            var isRequired = memberExpression.Member.GetCustomAttributes(typeof(RequiredAttribute), false).Length != 0;

            var displayAttribute = (DisplayAttribute[])memberExpression.Member.GetCustomAttributes(typeof(DisplayAttribute), false);

            var propertyName = metaData.PropertyName;
            var displayName = metaData.DisplayName ?? propertyName;

            string className = isThisEditor ? "ls-editor-textarea" : "";

            if (rvd.Any(q => q.Key == "class"))
            {
                rvd["class"] = rvd["class"] + $" {className} form-control text-box";
            }
            else
            {
                rvd["class"] = $"{className} form-control text-box";
            }


            var input = TextAreaExtensions.TextAreaFor(html, expression, rvd);


            var OuterDiv = new TagBuilder("div");
            OuterDiv.AddCssClass("form-group row m-form__group");


            var fieldLabel = new TagBuilder("label");
            fieldLabel.Attributes.Add("for", propertyName);
            fieldLabel.AddCssClass("col-form-label col-md-2");
            fieldLabel.InnerHtml = displayName + LabelAddText + (isRequired ? "<em>*</em>" : "");

            bool hasError = false;

            var validationSpan = new TagBuilder("span");
            validationSpan.AddCssClass("form-control-feedback");

            var modelState = html.ViewData.ModelState;

            if (modelState[propertyName] != null)
            {
                var error = modelState[propertyName].Errors.FirstOrDefault();
                if (error != null)
                {
                    hasError = true;
                    validationSpan.InnerHtml = error.ErrorMessage;
                }
            }

            if (hasError)
            {
                OuterDiv.AddCssClass("has-danger");
            }

            var inputWrapper = new TagBuilder("div");
            inputWrapper.AddCssClass("col-md-8");
            inputWrapper.InnerHtml = input + validationSpan.ToString();


            OuterDiv.InnerHtml = fieldLabel.ToString() + inputWrapper.ToString();

            return MvcHtmlString.Create(OuterDiv.ToString());
        }

        public static MvcHtmlString LsInput<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string LabelAddText = "",
            object htmlAttributes = null)
        {
            var rvd = new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var memberExpression = (MemberExpression)expression.Body;
            var isRequired = memberExpression.Member.GetCustomAttributes(typeof(RequiredAttribute), false).Length != 0;

            var displayAttribute = (DisplayAttribute[])memberExpression.Member.GetCustomAttributes(typeof(DisplayAttribute), false);

            var propertyName = metaData.PropertyName;
            var displayName = metaData.DisplayName ?? propertyName;



            var step = "1";

            var type = "text";

            switch (memberExpression.Type.Name)
            {
                case "Int32":
                    type = "number";
                    break;
                case "Decimal":
                    type = "number";
                    step = "0.01";
                    break;
                default:
                    break;
            }

            if (metaData.DataTypeName == "Password")
            {
                type = "password";
            }

            rvd.Add("type", type);

            if (rvd.Any(q => q.Key == "class"))
            {
                rvd["class"] = rvd["class"] + " form-control text-box single-line";
            }
            else
            {
                rvd["class"] = "form-control text-box single-line";
            }

            if (type == "number")
            {
                rvd.Add("step", step);
            }

            var input = InputExtensions.TextBoxFor(html, expression, rvd);


            var OuterDiv = new TagBuilder("div");
            OuterDiv.AddCssClass("form-group row m-form__group");


            var fieldLabel = new TagBuilder("label");
            fieldLabel.Attributes.Add("for", propertyName);
            fieldLabel.AddCssClass("col-form-label col-md-2");
            fieldLabel.InnerHtml = displayName + LabelAddText + (isRequired ? "<em>*</em>" : "");

            bool hasError = false;

            var validationSpan = new TagBuilder("span");
            validationSpan.AddCssClass("form-control-feedback");

            var modelState = html.ViewData.ModelState;

            if (modelState[propertyName] != null)
            {
                var error = modelState[propertyName].Errors.FirstOrDefault();
                if (error != null)
                {
                    hasError = true;
                    validationSpan.InnerHtml = error.ErrorMessage;
                }
            }

            if (hasError)
            {
                OuterDiv.AddCssClass("has-danger");
            }

            var inputWrapper = new TagBuilder("div");
            inputWrapper.AddCssClass("col-md-8");
            inputWrapper.InnerHtml = input + validationSpan.ToString();


            OuterDiv.InnerHtml = fieldLabel.ToString() + inputWrapper.ToString();

            return MvcHtmlString.Create(OuterDiv.ToString());
        }


        public static MvcHtmlString LsYoutube<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string LabelAddText = "",
            object htmlAttributes = null)
        {
            var rvd = new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var memberExpression = (MemberExpression)expression.Body;
            var isRequired = memberExpression.Member.GetCustomAttributes(typeof(RequiredAttribute), false).Length != 0;
            var displayAttribute = (DisplayAttribute[])memberExpression.Member.GetCustomAttributes(typeof(DisplayAttribute), false);

            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var propertyName = metaData.PropertyName;
            var displayName = metaData.DisplayName ?? propertyName;

            var className = "form-control text-box single-line ";
            string inputValue = (metaData.Model != null ? metaData.Model.ToString() : string.Empty);
            var iframeSrc = string.Empty;
            if (rvd.Any(q => q.Key == "class"))
            {
                className = className + rvd["class"];
            }

            TagBuilder tag = new TagBuilder("input");

            if (inputValue.Length == 11)
            {
                iframeSrc = "https://www.youtube.com/embed/" + inputValue;
                inputValue = "https://www.youtube.com/watch?v=" + inputValue;
            }

            tag.Attributes.Add("name", fullBindingName);
            tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("type", "text");
            tag.Attributes.Add("value", inputValue);
            tag.Attributes.Add("class", className);

            var input = new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));

            var OuterDiv = new TagBuilder("div");
            OuterDiv.AddCssClass("form-group row m-form__group youtube-video-container");


            var fieldLabel = new TagBuilder("label");
            fieldLabel.Attributes.Add("for", propertyName);
            fieldLabel.AddCssClass("col-form-label col-md-2");
            fieldLabel.InnerHtml = displayName + LabelAddText + (isRequired ? "<em>*</em>" : "");

            bool hasError = false;

            var validationSpan = new TagBuilder("span");
            validationSpan.AddCssClass("form-control-feedback");

            var modelState = html.ViewData.ModelState;

            if (modelState[propertyName] != null)
            {
                var error = modelState[propertyName].Errors.FirstOrDefault();
                if (error != null)
                {
                    hasError = true;
                    validationSpan.InnerHtml = error.ErrorMessage;
                }
            }

            if (hasError)
            {
                OuterDiv.AddCssClass("has-danger");
            }

            var iframeObj = new TagBuilder("iframe");
            iframeObj.Attributes.Add("class", "youtube-iframe");
            iframeObj.Attributes.Add("width", "100%");
            iframeObj.Attributes.Add("height", "400px");
            iframeObj.Attributes.Add("src", iframeSrc);

            var iframeContainer = new TagBuilder("div");
            iframeContainer.AddCssClass("youtube-video-box form-group row m-form__group");
            if (string.IsNullOrWhiteSpace(iframeSrc))
            {
                iframeContainer.AddCssClass("hidden");
            }

            iframeContainer.InnerHtml = iframeObj.ToString();

            var inputWrapper = new TagBuilder("div");
            inputWrapper.AddCssClass("col-md-8");
            inputWrapper.InnerHtml = input + validationSpan.ToString() + iframeContainer.ToString();

            OuterDiv.InnerHtml = fieldLabel.ToString() + inputWrapper.ToString();

            return MvcHtmlString.Create(OuterDiv.ToString());
        }

        public static MvcHtmlString LsVimeo<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression,
            string LabelAddText = "",
            object htmlAttributes = null)
        {
            var rvd = new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            var metaData = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var memberExpression = (MemberExpression)expression.Body;
            var isRequired = memberExpression.Member.GetCustomAttributes(typeof(RequiredAttribute), false).Length != 0;
            var displayAttribute = (DisplayAttribute[])memberExpression.Member.GetCustomAttributes(typeof(DisplayAttribute), false);

            var fieldName = ExpressionHelper.GetExpressionText(expression);
            var fullBindingName = html.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
            var fieldId = TagBuilder.CreateSanitizedId(fullBindingName);

            var propertyName = metaData.PropertyName;
            var displayName = metaData.DisplayName ?? propertyName;

            var className = "form-control text-box single-line ";
            string inputValue = (metaData.Model != null ? metaData.Model.ToString() : string.Empty);
            var iframeSrc = string.Empty;
            if (rvd.Any(q => q.Key == "class"))
            {
                className = className + rvd["class"];
            }

            TagBuilder tag = new TagBuilder("input");

            if (inputValue.Length == 9)
            {
                iframeSrc = "https://player.vimeo.com/video/" + inputValue;
                inputValue = "https://vimeo.com/" + inputValue;
            }


            tag.Attributes.Add("name", fullBindingName);
            tag.Attributes.Add("id", fieldId);
            tag.Attributes.Add("type", "text");
            tag.Attributes.Add("value", inputValue);
            tag.Attributes.Add("class", className);

            var input = new MvcHtmlString(tag.ToString(TagRenderMode.SelfClosing));

            var OuterDiv = new TagBuilder("div");
            OuterDiv.AddCssClass("form-group row m-form__group vimeo-video-container");


            var fieldLabel = new TagBuilder("label");
            fieldLabel.Attributes.Add("for", propertyName);
            fieldLabel.AddCssClass("col-form-label col-md-2");
            fieldLabel.InnerHtml = displayName + LabelAddText + (isRequired ? "<em>*</em>" : "");

            bool hasError = false;

            var validationSpan = new TagBuilder("span");
            validationSpan.AddCssClass("form-control-feedback");

            var modelState = html.ViewData.ModelState;

            if (modelState[propertyName] != null)
            {
                var error = modelState[propertyName].Errors.FirstOrDefault();
                if (error != null)
                {
                    hasError = true;
                    validationSpan.InnerHtml = error.ErrorMessage;
                }
            }

            if (hasError)
            {
                OuterDiv.AddCssClass("has-danger");
            }

            var iframeObj = new TagBuilder("iframe");
            iframeObj.Attributes.Add("class", "vimeo-iframe");
            iframeObj.Attributes.Add("width", "100%");
            iframeObj.Attributes.Add("height", "400px");
            iframeObj.Attributes.Add("src", iframeSrc);

            var iframeContainer = new TagBuilder("div");
            iframeContainer.AddCssClass("vimeo-video-box form-group row m-form__group");
            if (string.IsNullOrWhiteSpace(iframeSrc))
            {
                iframeContainer.AddCssClass("hidden");
            }

            iframeContainer.InnerHtml = iframeObj.ToString();

            var inputWrapper = new TagBuilder("div");
            inputWrapper.AddCssClass("col-md-8");
            inputWrapper.InnerHtml = input + validationSpan.ToString() + iframeContainer.ToString();

            OuterDiv.InnerHtml = fieldLabel.ToString() + inputWrapper.ToString();

            return MvcHtmlString.Create(OuterDiv.ToString());
        }

        public static IDisposable LsForm(this HtmlHelper helper, string Title, bool isMultiPart = false, string Controller = "", string Action = "")
        {
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            string enctype = string.Empty;
            string url = string.Empty;

            if (!string.IsNullOrWhiteSpace(Controller) && !string.IsNullOrWhiteSpace(Action))
            {
                url = urlHelper.Action(Action, Controller);
                url = "action=\"" + url + "\"";
            }
            if (isMultiPart)
                enctype = "multipart/form-data";

            var table = "<div class='m-portlet'>" +
                            "<div class='m-portlet__head'>" +
                                "<div class='m-portlet__head-caption'>" +
                                    "<div class='m-portlet__head-title'>" +
                                        "<h3 class='m-portlet__head-text'>" + Title + "</h3>" +
                                    "</div>" +
                                "</div>" +
                            "</div>" +
                            $"<form {url} class='m-form m-form--fit m-form--label-align-right m-form--group-seperator-dashed' enctype='{enctype}' id='ls-form' method='post'>" +
                                "<div class='m-portlet__body'>" +
                                    "<div class='form-horizontal'>";

            helper.ViewContext.Writer.Write(table);

            return new MyView(helper);
        }

        public static IDisposable LsTable(this HtmlHelper helper, string Title, string UpdateItemAction = "Update")
        {
            var table = "<div class='m-content'>" +
            "<div class='m-portlet m-portlet--mobile'>" +
                "<div class='m-portlet__head'>" +
                    "<div class='m-portlet__head-caption'>" +
                        "<div class='m-portlet__head-title'>" +
                            " <h3 class='m-portlet__head-text'>" +
                                Title +
                            "</h3>" +
                        "</div>" +
                    "</div>" +

                    "<div class='m-portlet__head-tools'>" +
                        "<ul class='m-portlet__nav'>" +
                            "<li class='m-portlet__nav-item'>" +
                                "<a href='" + UpdateItemAction + "' class='btn btn-primary m-btn m-btn--pill m-btn--custom m-btn--icon m-btn--air'>" +
                                    "<span>" +
                                        "<i class='la la-plus-circle'></i>" +
                                        "<span>" +
                                            "ახალი" +
                                        "</span>" +
                                    "</span>" +
                                "</a>" +
                            "</li>" +
                        "</ul>" +
                    "</div>" +
                "</div>" +
                "<div class='m-portlet__body'>";

            helper.ViewContext.Writer.Write(table);

            return new TableView(helper);
        }

        class TableView : IDisposable
        {
            private HtmlHelper helper;

            public TableView(HtmlHelper helper)
            {
                this.helper = helper;
            }

            public void Dispose()
            {

                string endTable = "</div>" +
                                    "</div>" +
                                    "</div>";

                this.helper.ViewContext.Writer.Write(endTable);
            }
        }
    }
}