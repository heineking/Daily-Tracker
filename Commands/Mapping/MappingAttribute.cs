using System;
using System.Collections.Generic;
using System.Text;

namespace Commands.Mapping
{
  public class MappingAttribute : Attribute {
    public string PropertyToName { get; set; }

    public MappingAttribute(string propertyToName) {
      PropertyToName = propertyToName;
    }
  }
}
