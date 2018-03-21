using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    /// <summary>
    /// Contains information about a page on the website
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Url for the page
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Content on the page
        /// </summary>
        public string Content { get; set; }
    }
}
