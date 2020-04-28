// Copyright (c) Cingulara LLC 2019 and Tutela LLC 2019. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.
using Microsoft.EntityFrameworkCore;
using web_api_controls.Models;

namespace web_api_controls.Database
{
    public class ControlsDBContext : DbContext  
    {  
        public ControlsDBContext(DbContextOptions<ControlsDBContext> options): base(options)  
        {  
    
        }  

        public DbSet<ControlSet> ControlSets { get; set; }
    }  
}