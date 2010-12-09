using System.Web.Routing;
using AutoMapper;
using AutoMapperAssist;
using Deg.Alt.Mvc.Actions;

namespace Deg.Alt.Mvc.Mappers
{
    public interface IRouteValueDictionaryToPageLocationMapper
    {
        PageLocation CreateInstance(RouteValueDictionary dictionary);
    }

    public class RouteValueDictionaryToPageLocationMapper : Mapper<RouteValueDictionary, PageLocation>, IRouteValueDictionaryToPageLocationMapper
    {
        private const string SectionId = "sectionId";
        private const string Action = "action";
        private const string PageId = "pageId";
        private const string FixedMarker = "Fixed";
        private const string Controller = "controller";

        private readonly IGetControllerForPageIdAction getControllerForPageIdAction;

        public RouteValueDictionaryToPageLocationMapper(IGetControllerForPageIdAction getControllerForPageIdAction)
        {
            this.getControllerForPageIdAction = getControllerForPageIdAction;
        }

        public override void DefineMap(IConfiguration configuration)
        {
            configuration.CreateMap<RouteValueDictionary, PageLocation>()
                .ForMember(dest => dest.SectionId, opt => opt.MapFrom(orig => orig[SectionId]))
                .ForMember(dest => dest.PageId, opt => opt.MapFrom(CalculateThePageId))
                .ForMember(dest => dest.Step, opt => opt.MapFrom(orig => orig[Action]))
                .ForMember(dest => dest.Controller, opt => opt.MapFrom(CalculateTheController));
        }

        private object CalculateTheController(RouteValueDictionary orig)
        {
            return ThisDictionaryHasBeenFixed(orig) ? orig[Controller] : getControllerForPageIdAction.GetController(orig[PageId] as string);
        }

        private object CalculateThePageId(RouteValueDictionary orig)
        {
            return ThisDictionaryHasBeenFixed(orig) ? orig[PageId] : orig[Controller];
        }

        public override PageLocation CreateInstance(RouteValueDictionary dictionary)
        {
            HandleThePageIdValueInTheDictionary(dictionary);

            var pageLocation = CreateThePageLocationUsingTheMappingRules(dictionary);

            HandleTheControllerValueInTheDictionary(dictionary, pageLocation);

            return pageLocation;
        }

        private static void HandleThePageIdValueInTheDictionary(RouteValueDictionary dictionary)
        {
            if (ThisDictionaryHasNotBeenFixed(dictionary))
                SetThePageIdInTheDictionary(dictionary);
        }

        private static void HandleTheControllerValueInTheDictionary(RouteValueDictionary dictionary, PageLocation pageLocation)
        {
            if (ThisDictionaryHasNotBeenFixed(dictionary))
                SetTheControllerInTheDictionary(dictionary, pageLocation);

            MarkTheDictionaryAsFixed(dictionary);
        }

        private PageLocation CreateThePageLocationUsingTheMappingRules(RouteValueDictionary dictionary)
        {
            return base.CreateInstance(dictionary);
        }

        private static void SetTheControllerInTheDictionary(RouteValueDictionary dictionary, PageLocation pageLocation)
        {
            dictionary[Controller] = pageLocation.Controller;
        }

        private static void MarkTheDictionaryAsFixed(RouteValueDictionary dictionary)
        {
            dictionary[FixedMarker] = "1";
        }

        private static void SetThePageIdInTheDictionary(RouteValueDictionary dictionary)
        {
            dictionary[PageId] = dictionary[Controller];
        }

        private static bool ThisDictionaryHasNotBeenFixed(RouteValueDictionary dictionary)
        {
            return ThisDictionaryHasBeenFixed(dictionary) == false;
        }

        private static bool ThisDictionaryHasBeenFixed(RouteValueDictionary dictionary)
        {
            return dictionary.ContainsKey(FixedMarker) && dictionary[FixedMarker] == "1";
        }
    }

    public class PageLocation
    {
        public string SectionId { get; set; }

        public string PageId { get; set; }

        public string Step { get; set; }

        public string Controller { get; set; }
    }
}