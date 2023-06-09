﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Library.CoreUI.DTOs
{
	public class AddBookDTO
	{
		public Guid BookId { get; set; }

		[Required]
		public string BookName { get; set; }

		[Required]
		public string ISBN { get; set; }

		[Required]
		public DateTime IssueDate { get; set; }

		public string Description { get; set; }
	}
}