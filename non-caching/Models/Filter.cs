// Copyright (c) Cingulara LLC 2020 and Tutela LLC 2020. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

namespace web_api_controls.Models
{
  public class Filter {

    public Filter () {
        impactLevel = "low";
    }
    public string impactLevel { get; set;}
    public bool pii { get; set;}
  }

}