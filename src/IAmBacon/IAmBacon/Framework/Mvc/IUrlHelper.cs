namespace IAmBacon.Framework.Mvc
{
    /// <summary>
    /// The URL helper.
    /// </summary>
    public interface IUrlHelper
    {
        /// <summary>
        /// Generates a fully qualified URL to an action method by using the specified action name and route values.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        /// <returns>The fully qualified URL to an action method.</returns>
        string Action(string actionName, object routeValues);

        /// <summary>
        /// Generates a fully qualified URL for the specified route values by using a route name.
        /// </summary>
        /// <param name="routeName"> The name of the route that is used to generate the URL.</param>
        /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        /// <param name="protocol">The protocol for the URL, such as "http" or "https".</param>
        /// <returns>The fully qualified URL.</returns>
        string RouteUrl(string routeName, object routeValues, string protocol);
    }
}
