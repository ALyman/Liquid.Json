using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Liquid.Json.Mvc {
    /// <summary>
    /// Provides extension methods for the System.Web.Mvc.Controller class
    /// </summary>
    public static class ControllerExtension {
        /// <summary>
        /// Creates a LiquidJsonActionResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <returns>The LiquidJsonActionResult for the specified object</returns>
        public static LiquidJsonActionResult<T> LiquidJson<T>(this Controller controller, T @object) {
            return new LiquidJsonActionResult<T>(@object);
        }

        /// <summary>
        /// Creates a LiquidJsonActionResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <param name="behavior">The behavior.</param>
        /// <returns>
        /// The LiquidJsonActionResult for the specified object
        /// </returns>
        public static LiquidJsonActionResult<T> LiquidJson<T>(this Controller controller, T @object, JsonRequestBehavior behavior) {
            return new LiquidJsonActionResult<T>(@object, behavior);
        }
        
        /// <summary>
        /// Creates a LiquidJsonActionResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>
        /// The LiquidJsonActionResult for the specified object
        /// </returns>
        public static LiquidJsonActionResult<T> LiquidJson<T>(this Controller controller, T @object, JsonSerializer serializer) {
            return new LiquidJsonActionResult<T>(@object, serializer);
        }

        /// <summary>
        /// Creates a LiquidJsonActionResult for the specified object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller">The controller.</param>
        /// <param name="object">The object.</param>
        /// <param name="behavior">The behavior.</param>
        /// <param name="serializer">The serializer.</param>
        /// <returns>
        /// The LiquidJsonActionResult for the specified object
        /// </returns>
        public static LiquidJsonActionResult<T> LiquidJson<T>(this Controller controller, T @object, JsonRequestBehavior behavior, JsonSerializer serializer) {
            return new LiquidJsonActionResult<T>(@object, behavior, serializer);
        }
    }
}
