using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTree.ContentNodeProvider.Models;
using Paragon.ContentTree.ContentNodeProvider.Validation;

namespace Paragon.ContentTree.ContentNodeProvider.Tests.Validation
{
	[TestClass]
	public class ContentTreeInputModelValidatorTests
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Name_is_required_when_action_is_Index()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel()
			                                	{
			                                		Action = "Index",
			                                	});

			Assert.AreEqual(1, result.Errors.Where(a => a.PropertyName == "Name").Count());
		}

		[TestMethod]
		public void UrlSegment_is_required_when_action_is_index()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel()
			                                	{
													Action = "Index",
			                                	});

			Assert.AreEqual(1, result.Errors.Where(a => a.PropertyName == "UrlSegment").Count());
		}

		[TestMethod]
		public void UrlSegment_is_not_required_when_action_is_not_index()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel()
			{
				Action = "z",
			});

			Assert.AreEqual(0, result.Errors.Where(a => a.PropertyName == "UrlSegment").Count());
		}

		[TestMethod]
		public void Name_is_not_required_when_action_is_not_index()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel()
			{
				Action = "z",
			});

			Assert.AreEqual(0, result.Errors.Where(a => a.PropertyName == "Name").Count());
		}

		[TestMethod]
		public void UrlSegment_is_required_when_action_is_null()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel());

			Assert.AreEqual(1, result.Errors.Where(a => a.PropertyName == "UrlSegment").Count());
		}

		[TestMethod]
		public void Name_is_required_when_action_is_null()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel());

			Assert.AreEqual(1, result.Errors.Where(a => a.PropertyName == "Name").Count());
		}
	}
}
