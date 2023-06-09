﻿using BookingLibrary.CoreUI.DTOs;
using Library.CoreUI.DTOs;
using Library.CoreUI.Utilities;
using Library.CoreUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Library.CoreUI.Models;
using Microsoft.Extensions.Options;

namespace Library.CoreUI.Controllers
{
	public class BookInventoryController : BaseController
	{
		public BookInventoryController(IOptions<ConfigurationModel> configAccessor) : base(configAccessor)
        {
		}

		[HttpPost]
		public ActionResult _AjaxBulkImported(BulkImportDTO dto)
		{
			List<Guid> newBookInventories = new List<Guid>();

			//hard code 10 repsitory id
			for (var i = 0; i < dto.Number; i++)
			{
				newBookInventories.Add(Guid.NewGuid());
			}

			var data = new NameValueCollection();
			data.Add("BookRepositoryIds", string.Join(",", newBookInventories.Select(p => p.ToString())));
			var commandKey = ApiRequest.Post<Guid>($"{_apiGatewayUrl}/api/books/{dto.BookId}/inventories", new ImportBookInventoryDTO
			{
				BookInventoryIds = newBookInventories
			});

			return Content(commandKey.ToString());
		}

		[HttpGet]
		public ActionResult Histories(Guid id)
		{
			var histories = ApiRequest.Get<List<BookInventoryHistoryViewModel>>($"{_apiGatewayUrl}/api/inventories/{id}/histories");
			return View(histories);
		}

		[HttpPut]
		public ActionResult InStore(InStoreBookInventoryDTO dto)
		{
			var commandKey = ApiRequest.Put<Guid>($"{_apiGatewayUrl}/api/inventories/{dto.BookInventoryId}/status", new
			{
				Status = 1,
				Notes = dto.Note,
				OccurredDate = dto.OccurredDate
			});

			return Content(commandKey.ToString());
		}

		[HttpPut]
		public ActionResult OutStore(OutStoreBookInventoryDTO dto)
		{
			var commandKey = ApiRequest.Put<Guid>($"{_apiGatewayUrl}/api/inventories/{dto.BookInventoryId}/status", new
			{
				Status = 2,
				Notes = dto.Note,
				OccurredDate = dto.OccurredDate
			});

			return Content(commandKey.ToString());
		}
	}
}