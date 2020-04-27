// Copyright (c) Cingulara LLC 2020 and Tutela LLC 2020. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel.DataAnnotations;

namespace web_api_controls.Models
{

  public class ControlSet {

    public ControlSet () {
      id = Guid.NewGuid(); // pk generated
    }
    [Key]
    public Guid id { get; set;}
    public string family { get; set;}
    public string number { get; set;}
    public string title { get; set;}
    public string priority { get; set;}
    public bool lowimpact { get; set;}
    public bool moderateimpact { get; set;}
    public bool highimpact { get; set;}
    public string supplementalGuidance { get; set;}

    public string subControlDescription { get; set;}
    public string subControlNumber { get; set;}
    
    // generate a sort string for 1 versus 10
    public string indexsort { get {
        string sort = "";
        int dash = number.IndexOf('-')+1;
        sort = number.Substring(0, dash); // gets you the XX and the -

        int space = number.IndexOf(" ", sort.Length);
        if (space > -1) {// there is something
          number = number.Substring(0, space);
        }
        int period = number.IndexOf(".", sort.Length);
        if (period > -1) { // there is a period so shorten it
          number = number.Substring(0, period);
        }
        if (number.Substring(dash).Length == 1) // need to pad the number with a 0}
          sort += "0" + number.Substring(dash);
        else 
          sort += number.Substring(dash);
        return sort;
      }
    }
  }

}