/// <reference path="../qunit-1.12.0.js" />

test("Evaluate email address", 1, function ()
{
	ok(IsValidEmailAddress1("a"), "'a' is not a vailid email address");
});