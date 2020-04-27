// Copyright (c) Cingulara LLC 2020 and Tutela LLC 2020. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using web_api_controls.Models;
using web_api_controls.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace web_api_controls.Controllers
{
    [Route("/")]
    public class ControlsController : Controller
    {
        private readonly ILogger<ControlsController> _logger;
        private readonly ControlsDBContext _context; 

        public ControlsController(ILogger<ControlsController> logger, ControlsDBContext context)
        {
            _logger = logger;
            _context = context;
        }        

        /// <summary>
        /// GET the full listing of NIST 800-53 controls based on impact level and PII boolean
        /// </summary>
        /// <param name="impactlevel">The impact level of low, medium, high to filter the controls returned</param>
        /// <param name="pii">A boolean to include the PII items or not</param>
        /// <returns>
        /// HTTP Status showing they were found and a list of control records for the NIST controls.
        /// </returns>
        /// <response code="200">Returns the newly updated item</response>
        /// <response code="400">If the get did not work correctly</response>
        /// <response code="404">If the impact passed is not valid</response>
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new [] {"impactlevel", "pii"})]
        public IActionResult GetAllControls(string impactlevel = "", bool pii = false)
        {
            try {
                _logger.LogInformation("Calling GetAllControls({0}, {1})", impactlevel, pii.ToString());
                var listing = _context.ControlSets.ToList();
                if (listing != null) {
                    var result = new List<ControlSet>(); // put all results in here
                    // figure out the impact level filter
                    if (impactlevel.Trim().ToLower() == "low")
                        result = listing.Where(x => x.lowimpact).ToList();
                    else if (impactlevel.Trim().ToLower() == "moderate")
                        result = listing.Where(x => x.moderateimpact).ToList();
                    else if (impactlevel.Trim().ToLower() == "high")
                        result = listing.Where(x => x.highimpact).ToList();
                    else
                        result = listing; // get all the data

                    // include things that are not P0 meaning not used, and that there is no low/moderate/high designation
                    // these should always be included where the combination of all "false" and not P0 = include them
                    result.AddRange(listing.Where(x => x.priority != "P0" && 
                        !x.lowimpact && !x.moderateimpact && !x.highimpact ).ToList());

                    // see if the PII  filter is true, and if so add in the PII family by appending that to the result from above
                    if (pii) {
                        result.AddRange(listing.Where(x => !string.IsNullOrEmpty(x.family) && x.family.ToLower() == "pii").ToList());
                    }

                    _logger.LogInformation("Called GetAllControls({0}, {1}) successfully", impactlevel, pii.ToString());
                    return Ok(result);
                }
                else {
                    _logger.LogWarning("Called GetAllControls({0}, {1}) but no control records listing returned", impactlevel, pii.ToString());
                    return NotFound(); // nothing loaded yet
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, "GetAllControls() Error listing all control sets. Please check the in memory database and XML file load.");
                return BadRequest();
            }
        }

        /// <summary>
        /// GET the full listing of NIST 800-53 major controls based on impact level and PII boolean
        /// </summary>
        /// <returns>
        /// HTTP Status showing they were found and a list of control records for the NIST controls.
        /// </returns>
        /// <response code="200">Returns the newly updated item</response>
        /// <response code="400">If the get did not work correctly</response>
        /// <response code="404">If the impact passed is not valid</response>
        [HttpGet("majorcontrols")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public IActionResult GetAllMajorControls()
        {
            try {
                _logger.LogInformation("Calling GetAllMajorControls()");
                var listing = _context.ControlSets.ToList();
                if (listing != null) {
                    var result = new List<ControlSet>(); // put all results in here
                    result = listing.Where(x => x.highimpact).ToList();
                    result.AddRange(listing.Where(x => !string.IsNullOrEmpty(x.family) && x.family.ToLower() == "pii").ToList());
                    _logger.LogInformation("Called GetAllMajorControls() successfully");
                    // get just the id, number, and title
                    result = result.GroupBy(x => new {x.number, x.title})
                            .Select(g => new ControlSet {
                                number = g.Key.number, 
                                title = g.Key.title}).ToList();
                    // return the distinct result
                    return Ok(result.Distinct().OrderBy(x => x.indexsort).ToList());
                }
                else {
                    _logger.LogWarning("Called GetAllMajorControls() but no control records listing returned");
                    return NotFound(); // nothing loaded yet
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex, "GetAllMajorControls() Error listing all control sets. Please check the in memory database and XML file load.");
                return BadRequest();
            }
        }
    }
}
