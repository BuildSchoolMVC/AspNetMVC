using AspNetMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC.Repository.FakeData
{
  public class SocialButtonRepository
  {
    public List<string> platforms { get; set; }

    public SocialButtonRepository()
    {
      platforms = new List<string> { "Google","Facebook","Line","Microsoft"};
    }

    public List<SocialButtonViewModel> CreateButtonList(int type)
    {
      return platforms.Select(x => new SocialButtonViewModel { 
        Platform = x,
        Type = type
      }).ToList();
    }
  }
}