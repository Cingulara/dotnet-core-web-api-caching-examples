// Copyright (c) Cingulara LLC 2020 and Tutela LLC 2020. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web_api_controls.Models
{

  public class Control {

    public Control () {
      childControls = new List<ChildControl>(); // child controls or statements listed
      id = Guid.NewGuid(); // pk generated
    }
    public string family { get; set;}
    public string number { get; set;}
    public string title { get; set;}
    public string priority { get; set;}
    public bool lowimpact { get; set;}
    public bool moderateimpact { get; set;}
    public bool highimpact { get; set;}
    public string supplementalGuidance { get; set;}
    public List<ChildControl> childControls { get; set; }
    [Key]
    public Guid id { get; set;}
  }

  public class ChildControl {
    public ChildControl() {
      id = Guid.NewGuid(); // pk generated
    }

    public string description { get; set;}
    public string number { get; set;}
    [Key]
    public Guid id { get; set;}
  }

}