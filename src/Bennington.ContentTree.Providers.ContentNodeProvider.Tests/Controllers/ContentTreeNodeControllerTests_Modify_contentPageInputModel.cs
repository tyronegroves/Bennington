using System;
using System.Web.Mvc;
using AutoMoq;
using Bennington.ContentTree.Domain.Commands;
using Bennington.ContentTree.Providers.ContentNodeProvider.Context;
using Bennington.ContentTree.Providers.ContentNodeProvider.Controllers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Mappers;
using Bennington.ContentTree.Providers.ContentNodeProvider.Models;
using Bennington.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleCqrs.Commanding;

namespace Bennington.ContentTree.Providers.ContentNodeProvider.Tests.Controllers
{
	[TestClass]
	public class ContentTreeNodeControllerTests_Modify_ContentTreeNodeInputModel
	{
		private AutoMoqer mocker;

		[TestInitialize]
		public void Init()
		{
			mocker = new AutoMoqer();
		}

		[TestMethod]
		public void Sets_view_model_action_to_Modify_when_ModelState_when_ModelState_is_not_valid()
		{
			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.ModelState.AddModelError("key", "error");
			var result = contentTreeNodeController.Modify(new ContentTreeNodeInputModel());

			Assert.AreEqual("Modify", ((ContentTreeNodeViewModel)((ViewResult)result).ViewData.Model).Action);
		}

		[TestMethod]
		public void Returns_redirect_result_to_content_tree_index_when_ModelState_is_valid_and_input_model_action_is_saveandexit()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
											{
												TreeNodeId = Guid.NewGuid().ToString(),
												PageId = Guid.NewGuid().ToString(),
												Name = "name",
												Action = "Index",
												FormAction = "Save and Exit"
											};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>()
				.Setup(a => a.CreateInstance(contentTreeNodeInputModel)).Returns(new ContentTreeNode()
																					{
																						Name = "name",
																					});

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Modify(contentTreeNodeInputModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void Returns_redirect_result_when_ModelState_is_valid()
		{
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
														{
															PageId = Guid.NewGuid().ToString(),
															TreeNodeId = Guid.NewGuid().ToString(),
															Name = "name",
														};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>()
				.Setup(a => a.CreateInstance(contentTreeNodeInputModel)).Returns(new ContentTreeNode()
																					{
																						Name = "name",
																					});

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			var result = contentTreeNodeController.Modify(contentTreeNodeInputModel);

			Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
		}

		[TestMethod]
		public void Returns_view_model_with_input_model_set_to_the_same_as_was_passed_in_when_ModelState_is_invalid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var contentTreeNodeInputModel = new ContentTreeNodeInputModel()
													{
														TreeNodeId = treeNodeId.ToString(),
														Name = "name",
														Action = "Index",
													};
			mocker.GetMock<IContentTreeNodeInputModelToContentTreeNodeMapper>()
				.Setup(a => a.CreateInstance(contentTreeNodeInputModel)).Returns(new ContentTreeNode()
																					{
																						Name = "name",
																					});

			var contentTreeNodeController = mocker.Resolve<ContentTreeNodeController>();
			contentTreeNodeController.ModelState.AddModelError("key", "error");
			var result = contentTreeNodeController.Modify(contentTreeNodeInputModel);

			var contentTreeNodeViewModel = (ContentTreeNodeViewModel)((ViewResult) result).ViewData.Model;
			Assert.AreEqual(contentTreeNodeInputModel, contentTreeNodeViewModel.ContentTreeNodeInputModel);
		}

		[TestMethod]
		public void Sends_ModifyPage_command_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
										{
											Name = "name",
											PageId = Guid.NewGuid().ToString(),
											TreeNodeId = treeNodeId.ToString(),
											Action = "Index"
										};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<ModifyPageCommand>()), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_HeaderText_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										Name = "name",
										HeaderText = "header text",
										Action = "Index",
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.HeaderText == inputModel.HeaderText)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_Hidden_property_value_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				PageId = Guid.NewGuid().ToString(),
				TreeNodeId = treeNodeId.ToString(),
				Name = "name",
				Hidden = true,
				Action = "Index",
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.Hidden == inputModel.Hidden)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_Active_property_value_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				PageId = Guid.NewGuid().ToString(),
				TreeNodeId = treeNodeId.ToString(),
				Name = "name",
				Active = true,
				Action = "Index",
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.Active == inputModel.Active)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_Body_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
										{
											PageId = Guid.NewGuid().ToString(),
											TreeNodeId = treeNodeId.ToString(),
											Body = "content",
											Action = "Index",
										};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.Body == inputModel.Body)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_AggregateRootId_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										Body = "content",
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										Action = "Index"
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.AggregateRootId.ToString() == inputModel.PageId)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_ParentId_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
										ParentTreeNodeId = "123"
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.ParentId.ToString() == inputModel.ParentTreeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_Sequence_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										Sequence = 100,
										Action = "Index",
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.Sequence == inputModel.Sequence)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_UrlSegment_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										UrlSegment = "test",
										Action = "Index",
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.UrlSegment == inputModel.UrlSegment)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_StepId_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
										{
											PageId = Guid.NewGuid().ToString(),
											TreeNodeId = treeNodeId.ToString(),
											Action = "Index"
										};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.ActionId == inputModel.Action)), Times.Once());
		}

		[TestMethod]
		public void Sends_ModifyPage_command_with_correct_TreeNodeId_when_ModelState_is_valid()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										Action = "Index"
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<ModifyPageCommand>(b => b.TreeNodeId.ToString() == inputModel.TreeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Only_sends_a_single_ModifyPage_command()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										PageId = Guid.NewGuid().ToString(),
										TreeNodeId = treeNodeId.ToString(),
										Action = "Index"
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<ModifyPageCommand>()), Times.Once());
		}

		[TestMethod]
		public void Does_not_send_ModifyPage_command_when_ModelState_is_not_valid()
		{
			var inputModel = new ContentTreeNodeInputModel()
										{
											Name = "name",
											TreeNodeId = "treeNodeId",
											Action = "Index"
										};

			var controller = mocker.Resolve<ContentTreeNodeController>();
			controller.ModelState.AddModelError("key", "error");
			controller.Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<ModifyPageCommand>()), Times.Never());
		}

		[TestMethod]
		public void Does_not_send_ModifyPage_command_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										TreeNodeId = treeNodeId.ToString(),
										Action = "Confirmation"
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<ModifyPageCommand>()), Times.Never());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
									{
										TreeNodeId = treeNodeId.ToString(),
										Action = "Confirmation"
									};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.IsAny<CreatePageCommand>()), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_PageId_from_IGuidGetter_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var pageId = Guid.NewGuid();
			mocker.GetMock<IGuidGetter>().Setup(a => a.GetGuid()).Returns(pageId);
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation"
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.PageId == pageId)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_TreeNodeId_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation"
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.TreeNodeId == treeNodeId)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_Body_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				Body = "content",
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Body == inputModel.Body)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_Hidden_property_value_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				Hidden = true,
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Hidden == inputModel.Hidden)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_Active_property_value_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				Active = true,
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Active == inputModel.Active)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_HeaderText_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				HeaderText = "HeaderText"
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.HeaderText == inputModel.HeaderText)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_Name_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				Name = "name"
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Name == inputModel.Name)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_UrlSegment_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				UrlSegment = "url",
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.UrlSegment == inputModel.UrlSegment)), Times.Once());
		}

		[TestMethod]
		public void Sends_CreatePageCommand_with_correct_Action_when_attempting_to_modify_a_page_that_does_not_exist()
		{
			var treeNodeId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				TreeNodeId = treeNodeId.ToString(),
				Action = "Confirmation",
				UrlSegment = "url",
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<CreatePageCommand>(b => b.Action == inputModel.Action)), Times.Once());
		}

		[TestMethod]
		public void Sends_PagePublishedCommand_with_correct_PageId_set_when_input_model_FormAction_property_begins_with_Publish()
		{
			var treeNodeId = Guid.NewGuid();
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				PageId = pageId.ToString(),
				TreeNodeId = treeNodeId.ToString(),
				Action = "Index",
				FormAction = "Publish and Exit"
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<PublishPageCommand>(b => b.PageId == pageId)), Times.Once());
		}

		[TestMethod]
		public void Does_not_send_PagePublishedCommand_with_correct_PageId_set_when_input_model_FormAction_property_does_not_begin_with_Publish()
		{
			var treeNodeId = Guid.NewGuid();
			var pageId = Guid.NewGuid();
			mocker.GetMock<IContentTreeNodeContext>().Setup(a => a.GetContentTreeNodesByTreeId(treeNodeId.ToString()))
				.Returns(new ContentTreeNode[]
				         	{
				         		new ContentTreeNode()
				         			{
				         				TreeNodeId = treeNodeId.ToString(),
										Action = "Index",
				         			}, 
							});
			var inputModel = new ContentTreeNodeInputModel()
			{
				PageId = pageId.ToString(),
				TreeNodeId = treeNodeId.ToString(),
				Action = "Index",
				FormAction = "Exit"
			};

			mocker.Resolve<ContentTreeNodeController>().Modify(inputModel);

			mocker.GetMock<ICommandBus>().Verify(a => a.Send(It.Is<PublishPageCommand>(b => b.PageId == pageId)), Times.Never());
		}
	}
}
