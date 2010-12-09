using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paragon.ContentTreeNodeProvider.Models;
using Paragon.ContentTreeNodeProvider.Validation;

namespace Paragon.ContentTreeNodeProvider.Tests
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
		public void Name_is_required()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel());

			Assert.AreEqual(1, result.Errors.Where(a => a.PropertyName == "Name").Count());
		}

		[TestMethod]
		public void UrlSegment_is_required()
		{
			var validator = mocker.Resolve<ContentTreeInputModelValidator>();
			var result = validator.Validate(new ContentTreeNodeInputModel());

			Assert.AreEqual(1, result.Errors.Where(a => a.PropertyName == "UrlSegment").Count());
		}
	}
}
