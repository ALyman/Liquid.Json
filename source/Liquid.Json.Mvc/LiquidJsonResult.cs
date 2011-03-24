using System;
using System.Web;
using System.Web.Mvc;

namespace Liquid.Json.Mvc
{
    /// <summary>
    /// An action that uses the Liquid.Json serialization API to serialize an object
    /// </summary>
    /// <typeparam name="T">The type of the result</typeparam>
    public class LiquidJsonResult<T> : ActionResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidJsonResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="object">The result object.</param>
        public LiquidJsonResult(T @object)
            : this(@object, JsonRequestBehavior.DenyGet, null) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidJsonResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="object">The result object.</param>
        /// <param name="behavior">The behavior of the action.</param>
        public LiquidJsonResult(T @object, JsonRequestBehavior behavior)
            : this(@object, behavior, null) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidJsonResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="object">The result object.</param>
        /// <param name="serializer">The serializer.</param>
        public LiquidJsonResult(T @object, JsonSerializer serializer)
            : this(@object, JsonRequestBehavior.DenyGet, serializer) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="LiquidJsonResult&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="object">The result object.</param>
        /// <param name="behavior">The behavior of the action.</param>
        /// <param name="serializer">The serializer.</param>
        public LiquidJsonResult(T @object, JsonRequestBehavior behavior, JsonSerializer serializer)
        {
            Object = @object;
            Behavior = behavior;
            Serializer = serializer ?? new JsonSerializer();
        }

        /// <summary>
        /// Gets or sets the result object.
        /// </summary>
        /// <value>The result object.</value>
        public T Object { get; private set; }

        /// <summary>
        /// Gets or sets the behavior of the action.
        /// </summary>
        /// <value>The behavior of the action.</value>
        public JsonRequestBehavior Behavior { get; private set; }

        /// <summary>
        /// Gets or sets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        public JsonSerializer Serializer { get; private set; }

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (Behavior == JsonRequestBehavior.DenyGet
                &&
                context.HttpContext.Request.HttpMethod.ToUpper() == "GET")
                throw new NotSupportedException("JsonActionResult with Behavior=DenyGet does not support the GET method");

            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = "application/json";
            Serializer.Serialize(Object, response.Output);
        }
    }
}