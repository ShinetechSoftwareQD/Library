﻿using System;

namespace Library.CoreUI.ViewModels
{
	public class BookInventoryHistoryViewModel
	{
		public Guid HistoryId { get; set; }

		public string Notes { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}