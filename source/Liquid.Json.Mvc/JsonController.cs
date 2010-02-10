using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Liquid.Json.Mvc {
    /// <summary>
    /// Provides extension methods for the System.Web.Mvc.Controller class
    /// </summary>
    public abstract class JsonController : Controller {
        /// <summary>
        /// Creates a LiquidJsonResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <returns>The LiquidJsonResult for the specified object</returns>
        protected LiquidJsonResult<T> Json<T>(T @object) {
            return new LiquidJsonResult<T>(@object);
        }

        /// <summary>
        /// Creates a LiquidJsonResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns>
        /// The LiquidJsonResult for the specified object
        /// </returns>
        protected LiquidJsonResult<T> Json<T>(T @object, JsonRequestBehavior behavior) {
            return new LiquidJsonResult<T>(@object, behavior);
        }
        
        /// <summary>
        /// Creates a LiquidJsonResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>
        /// The LiquidJsonResult for the specified object
        /// </returns>
        protected LiquidJsonResult<T> Json<T>(T @object, JsonSerializer serializer) {
            return new LiquidJsonResult<T>(@object, serializer);
        }

        /// <summary>
        /// Creates a LiquidJsonResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <param name="behavior">The behavior.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>
        /// The LiquidJsonResult for the specified object
        /// </returns>
        protected LiquidJsonResult<T> Json<T>(T @object, JsonRequestBehavior behavior, JsonSerializer serializer) {
            return new LiquidJsonResult<T>(@object, behavior, serializer);
        }
    }
}
