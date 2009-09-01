namespace CodeBetter.Canvas.Web
{
    using System.Collections.Generic;
    using MvcContrib.FluentHtml;
    using MvcContrib.FluentHtml.Behaviors;
    using Spark.Web.Mvc;

    public abstract class ApplicationViewPage<T> : SparkView<T>, IViewModelContainer<T> where T : class
    {
        private readonly List<IBehaviorMarker> _behaviors = new List<IBehaviorMarker>();

        protected ApplicationViewPage()
        {
            _behaviors.Add(new ValidationBehavior(() => ViewData.ModelState));
        }

        public string HtmlNamePrefix { get; set; }

        public T ViewModel
        {
            get { return Model; }
        }

        public IEnumerable<IBehaviorMarker> Behaviors
        {
            get { return _behaviors; }
        }
    }

}