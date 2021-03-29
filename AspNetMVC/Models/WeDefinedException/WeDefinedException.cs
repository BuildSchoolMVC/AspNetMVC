using System;

namespace AspNetMVC.Models.WeDefinedException {
	public class PermissionException : Exception {
		public PermissionException() { }
		public PermissionException(string message) : base(message) { }
	}
}