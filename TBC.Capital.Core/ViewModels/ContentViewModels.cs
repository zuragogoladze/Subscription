using TBC.Capital.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TBC.Capital.Core.ViewModels
{
    public class UserInRoleVM
    {
        public string Id { get; set; }
        [Display(Name = "როლი")]
        public IEnumerable<string> UserRole { get; set; }
        [Display(Name = "სახელი")]
        public string UserName { get; set; }
        [Display(Name = "ელ.ფოსტა")]
        public string UserEmail { get; set; }
    }

    public class GalleryVM
    {
        public int Id { get; set; }
        public int? Order { get; set; }
        public string Name { get; set; }
    }
    public class ModelStateErrorsVM
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class LanguageVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class ImagesAdminVM
    {
        [Required]
        [Display(Name = "Url")]
        public string Url { get; set; }
    }

    public class CropperVM
    {
        public int Id { get; set; }
        public string FieldName { get; set; }
        public string ImageString { get; set; }
        public string ImageExistingValue { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string ValidationsMessage { get; set; }
        public string LabelText { get; set; }
    }

    public class GallerySizeVM
    {
        public string Width { get; set; }
        public string Heigth { get; set; }
    }

    public class ShareDataVM
    {
        public ShareDataVM()
        {
            Id = 0;
        }
        public int Id { get; set; }
        [Display(Name = "შეარ სურათი")]
        public string Image { get; set; }
        [Display(Name = "ანალიტიკის კოდი")]
        public string AnalyticsScript { get; set; }
        [Display(Name = "პიქსელის კოდი")]
        public string PixelScript { get; set; }

        [Display(Name = "ქეივორდები (გამოყავიტ ,_ით)")]
        public string MetaKeyWords { get; set; }

        public List<ShareDataVMT> Translations { get; set; }
    }

    public class ShareDataVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "og თაითლი")]
        public string Title { get; set; }
        [Display(Name = "og დესქრიფშენი")]
        public string Description { get; set; }

        [Display(Name = "მეტა დესქრიფშენი")]
        public string MetaDescription { get; set; }
    }

    public class ShareDataVML
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string AnalyticsScript { get; set; }
        public string PixelScript { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
    }
    // working VM
    public class MainPageSliderVML
    {
        public int Id { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        [Display(Name = "ტექსტი")]
        public string Text { get; set; }
        [Display(Name = "გამოქვეყნებულია")]
        public bool IsPublished { get; set; }
        [Display(Name = "ლინკი")]
        public string Url { get; set; }

    }
    // working VM
    public class MainPageSliderVM
    {
        public int Id { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        [Display(Name = "მთავარი სურათის გამუქება")]
        public bool IsDarkMode { get; set; }
        [Display(Name = "გამოჩენა")]
        public bool IsPublished { get; set; }
        [Display(Name = "Youtube ვიდეოს ლინკი")]
        public string YoutubeId { get; set; }
        [Display(Name = "Vimeo ვიდეოს ლინკი")]
        public string VimeoId { get; set; }
        public List<MainPageSliderVMT> Translations { get; set; }
    }
    // working VM
    public class MainPageSliderVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "სახელი")]
        public string Text { get; set; }
        [Display(Name = "ლინკი")]
        public string Url { get; set; }
    }
    // working VM
    public class ContactVM
    {
        public int Id { get; set; }
        [Display(Name = "ტელეფონი")]
        public string Phone { get; set; }
        [Display(Name = "ფაქსი")]
        public string Fax { get; set; }
        [Display(Name = "იმეილი")]
        public string Email { get; set; }
        public LocationVM Location { get; set; }
        public List<ContactVMT> Translations { get; set; }
    }
    // working VM
    public class ContactVMT
    {
        public string LanguageCode { get; set; }
        [Display(Name = "მისამართი")]
        public string Address { get; set; }
    }
    // working VM
    public class LocationVM
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
    // working VM
    public class PageInfoVM
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string CoverImage { get; set; }
        public List<PageInfoVMT> Translations { get; set; }
    }
    // working VM
    public class PageInfoVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "გვერდის სახელი")]
        public string Title { get; set; }
        [Display(Name = "სათაური ქავერზე")]
        public string CoverTitle { get; set; }
        [Display(Name = "კონტენტი")]
        public string Description { get; set; }
    }
    // working VM
    public class AboutUsInfoMainPageVM
    {
        public int Id { get; set; }
        [Display(Name = "ლინკი")]
        public string Url { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        [Display(Name = "გამოქვეყნება")]
        public bool IsPublished { get; set; }
        public List<AboutUsInfoMainPageVMT> Translations { get; set; }
    }
    // working VM
    public class AboutUsInfoMainPageVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
    }
    // working VM
    public class AboutUsInfoMainPageVML
    {
        public int Id { get; set; }
        [Display(Name = "ლინკი")]
        public string Url { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        [Display(Name = "გამოქვეყნებულია")]
        public bool IsPublished { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
    }
    // working VM
    public class SessionMessageVM
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
    // working VM
    public class TeamMemberVML
    {
        public int Id { get; set; }
        [Display(Name = "სახელი")]
        public string Name { get; set; }
        [Display(Name = "პოზიცია")]
        public string Position { get; set; }
        [Display(Name = "საკონტაქტო ნომერი")]
        public string PhoneNumber { get; set; }
        [Display(Name = "იმეილი")]
        public string Email { get; set; }
    }
    // working VM
    public class TeamMemberVM
    {
        public int Id { get; set; }
        [Display(Name = "საკონტაქტო ნომერი")]
        public string PhoneNumber { get; set; }
        [Display(Name = "იმეილი")]
        public string Email { get; set; }
        public List<TeamMemberVMT> Translations { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
    }
    // working VM
    public class TeamMemberVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "სახელი")]
        public string Name { get; set; }
        [Display(Name = "პოზიცია")]
        public string Position { get; set; }
    }
    // working VM
    public class CoverVM
    {
        public int Id { get; set; }
        [Display(Name = "ლინკი")]
        public string Url { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        public List<CoverVMT> Translations { get; set; }
    }
    // working VM
    public class CoverVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
    }
    // working VM
    public class ResearchVM
    {
        public ResearchVM()
        {
            Translations = new List<ResearchVMT>();
            PublishDate = DateTime.Now;
            AttachedFiles = new List<AttachedFilesVM>();
        }
        public int Id { get; set; }
        [Display(Name = "გამოქვეყნების თარიღი")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        [Display(Name = "ქავერის სურათი")]
        public string CoverImage { get; set; }
        [Display(Name = "Share სურათი")]
        public string ShareImage { get; set; }
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "გამოქვეყნება")]
        public bool IsPublished { get; set; }
        [Display(Name = "თეგები")]
        public List<ResearchVMT> Translations { get; set; }
        public List<AttachedFilesVM> AttachedFiles { get; set; }
    }
    // working VM
    public class ResearchVMT
    {
        public int Id { get; set; }
        public string LanguageCode { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
    }
    // working VM
    public class ResearchVML
    {
        public int Id { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "გამოქვეყნების თარიღი")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "გამოქვეყნებულია")]
        public bool IsPublished { get; set; }
    }
    // working VM
    public class TagsVM
    {
        public int Id { get; set; }
        [Translations(ErrorMessage = "ერთერთ ენაზე მაინც უნდა იყს ინფორმაცია ჩამატებული")]
        public List<TagsVMT> Translations { get; set; }
    }
    // working VM
    public class TagsVMT
    {
        public string LanguageCode { get; set; }
        [Display(Name = "თეგის სახელი")]
        public string Name { get; set; }
    }
    // working VM
    public class TagsVML
    {
        public int Id { get; set; }
        [Display(Name = "დასახელება")]
        public string Name { get; set; }
    }
    // working VM
    public class VacancyVM
    {
        public int Id { get; set; }
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "გამოქვეყნება")]
        public bool IsPublished { get; set; }
        [Display(Name = "დასრულების თარიღი")]
        public DateTime EndDate { get; set; }
        public List<VacancyVMT> Translations { get; set; }
    }
    // working VM
    public class VacancyVMT
    {
        public string LanguageCode { get; set; }
        [Display(Name = "პოზიციის დასახელება")]
        public string Position { get; set; }
        [Display(Name = "ვაკანსიის აღწერა")]
        public string Description { get; set; }
        [Display(Name = "ვაკანსიის მისამართი")]
        public string Location { get; set; }
        [Display(Name = "დასაქმების ფორმა (მაგ: Full-time)")]
        public string Schedule { get; set; }
    }
    // working VM
    public class VacancyVML
    {
        public int Id { get; set; }
        [Display(Name = "პოზიციის დასახელება")]
        public string Position { get; set; }
        [Display(Name = "დამატების თარიღი")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "ვაკანსიის მისამართი")]
        public string Location { get; set; }
        [Display(Name = "დასაქმების ფორმა (მაგ: Full-time)")]
        public string Schedule { get; set; }
        [Display(Name = "გამოქვეყნებულია")]
        public bool IsPublished { get; set; }
        [Display(Name = "დასრულების თარიღი")]
        public DateTime EndDate { get; set; }
    }
    public class SortableAdminViewModel
    {
        public int? item_id { get; set; }
        public int? parent_id { get; set; }
    }

    public class MenuVM
    {
        public MenuVM()
        {
            Translations = new List<MenuVMT>();
        }
        public int Id { get; set; }
        [Display(Name = "გამოჩენა")]
        public bool Active { get; set; }
        public int HeaderOrder { get; set; }
        public int FooterOrder { get; set; }
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "Header/Footer")]

        public MenuType? Type { get; set; }


        public List<MenuVMT> Translations { get; set; }
    }

    public class MenuVMT
    {
        public string LanguageCode { get; set; }
        [Display(Name = "გარე ლინკი")]
        public string Url { get; set; }
        [Display(Name = "დასახელება")]
        public string Name { get; set; }
    }

    public class MenuVML
    {
        public int Id { get; set; }
        [Display(Name = "გამოჩენა")]
        public bool Active { get; set; }
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "გარე ლინკი")]
        public string Url { get; set; }
        [Display(Name = "დასახელება")]
        public string Name { get; set; }


        public List<MenuVML> Children { get; set; }
    }
    // working VM
    public class NewsVM
    {
        public NewsVM()
        {
            Translations = new List<NewsVMT>();
            TagsId = new int[] { };
            AttachedFiles = new List<AttachedFilesVM>();
        }
        public int Id { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        [Display(Name = "ქავერის სურათი")]
        public string CoverImage { get; set; }
        [Display(Name = "Share სურათი")]
        public string ShareImage { get; set; }
        [Required(ErrorMessage = "სლაგი სავალდებულო ველია!")]
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "გამოქვეყნება")]
        public bool IsPublished { get; set; }
        [Display(Name = "გამოქვეყნების თარიღი")]
        public DateTime PublishDate { get; set; }
        public int[] TagsId { get; set; }
        public List<NewsVMT> Translations { get; set; }
        public List<AttachedFilesVM> AttachedFiles { get; set; }
    }
    // working VM
    public class NewsVMT
    {
        public string LanguageCode { get; set; }

        //[Required(ErrorMessage = "სახელწოდება სავალდებულოა!")]
        [Display(Name = "სახელწოდება")]
        public string Title { get; set; }
        //[Required(ErrorMessage = "აღწერა სავალდებულოა!")]
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
    }
    // working VM
    public class NewsVML
    {
        public int Id { get; set; }
        [Display(Name = "სლაგი")]
        public string Slug { get; set; }
        [Display(Name = "სახელწოდება")]
        public string Title { get; set; }
        [Display(Name = "გამოქვეყნების თარიღი")]
        public DateTime PublishDate { get; set; }
        [Display(Name = "გამოქვეყნებულია")]
        public bool IsPublished { get; set; }
    }
    // working VM
    public class AttachedFilesVM
    {
        public AttachedFilesVM()
        {
            Translations = new List<AttachedFilesVMT>();
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public int? Order { get; set; }
        [Translations(ErrorMessage = "ფაილების სახელწოდება ერთერთ ენაზე მაინც უნდა იყს ჩამატებული")]
        public List<AttachedFilesVMT> Translations { get; set; }
    }
    // working VM
    public class AttachedFilesVMT
    {
        public string LanguageCode { get; set; }
        public string File { get; set; }
        [Display(Name = "დასახელება")]
        public string Name { get; set; }
    }
    public class ContactFormVM
    {
        public string Id { get; set; }
        [Display(Name = "სახელი")]
        public string Name { get; set; }

        [Display(Name = "გვარი")]
        public string Surname { get; set; }

        [Display(Name = "ელ. ფოსტა")]
        public string Email { get; set; }

        [Display(Name = "ტელეფონის ნომერი")]
        public string PhoneNumber { get; set; }

        [Display(Name = "მესიჯის ტექსტი")]
        public string Message { get; set; }
    }
    public class VacancyFormVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Comment { get; set; }
        public string FileName { get; set; }
    }
    public class ProductVML
    {
        public string Id { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }

        [Display(Name = "აღწერა")]
        public string Description { get; set; }

        [Display(Name = "ლინკი")]
        public string Url { get; set; }
    }
    // working VM
    public class ProductVM
    {
        public int Id { get; set; }
        public List<ProductVMT> Translations { get; set; }
    }
    // working VM
    public class ProductVMT
    {
        public string LanguageCode { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Display(Name = "ლინკი")]
        public string Url { get; set; }
    }
    // working VM
    public class PartnerVM
    {
        public int Id { get; set; }
        [Display(Name = "სურათი")]
        public string Image { get; set; }
        public List<PartnerVMT> Translations { get; set; }
    }
    // working VM
    public class PartnerVMT
    {
        public string LanguageCode { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
    }
    public class PartnerVML
    {
        public string Id { get; set; }
        [Display(Name = "სათაური")]
        public string Title { get; set; }
        [Display(Name = "აღწერა")]
        public string Description { get; set; }
        [Display(Name = "ლინკი")]
        public string Image { get; set; }
    }
    public class SubscriberVML
    {
        public string Id { get; set; }
        [Display(Name = "ელ. ფოსტა")]
        public string Email { get; set; }
        [Display(Name = "გამოწერილია კვლევები")]
        public bool IsSubscribedToResearches { get; set; }
        [Display(Name = "გამოწერის ტიპი (დღეები)")]
        public int SubscriptionTime { get; set; }
        [Display(Name = "სიახლეების თეგები")]
        public List<TagsVML> Tags { get; set; }
    }
}




