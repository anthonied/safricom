General Library 1.0.0.8 - Solution Server


A library with some useful methods and extensions


Version 1.0.0.8 (2013-03-12)
===============

String Extensions - TrimIfNotNull
-	Returns null if a string is the null value
-	If a string is not the null value, it removes all leading and trailing white space characters like: space, line feed (\n), carriage return (\r), tab (\t), vertical tab (\v), ...


Version 1.0.0.7 (2012-09-07)
===============

String Extensions - CompareTo
-	Compares this instance with specified string
-	Optional ignore case parameter

String Extensions - IsEqualTo
-	Determines whether this instance and another specified string have the same value
-	Optional ignore case parameter

String Extensions - FormatWith
-	Replaces the format item in a specified composite format string with the string representation of a corresponding object in a specified array


Version 1.0.0.6 (2012-09-04)
===============

String Extensions - IsNullOrEmpty
-	Checks if a string is the null value or empty ("")

String Extensions - IsNullOrWhiteSpace
-	Checks if a string is the null value, empty ("") or contains only white space characters like: space, line feed (\n), carriage return (\r), tab (\t), vertical tab (\v), ...


Version 1.0.0.5 (2012-09-03)
===============

DateTime Extensions - SetTime
-	Sets the time component

TimeSpan Extensions - ToDateTime
-	Converts to DateTime using a given date as start date
-	Uses today's date as start date if no date was given


Version 1.0.0.4 (2012-09-01)
===============

String Extensions - IsNull
-	Checks if a string is the null value

String Extensions - IsEmpty
-	Checks if a string is empty ("")

String Extensions - IsWhiteSpace
-	Checks if a string is empty ("") or contains only white space characters like: space, line feed (\n), carriage return (\r), tab (\t), vertical tab (\v), ...


Version 1.0.0.3 (2012-08-31)
===============

Added release notes to SSDevelopment folder
-	This folder should be excluded/not included in setups/releases


Version 1.0.0.2 (2012-08-30)
===============

Added intellisense comments to all available methods


Version 1.0.0.1 (2012-08-30)
===============

String Extensions
-	Removed namespace SSLibrary.Extensions
-	No using statement required to be available

(Nullable) DateTime Extensions
-	Changed namespace to System
-	No extra using statement required to be available

XmlNode Extensions
-	Changed namespace to System.Xml
-	No extra using statement required to be available

XmlNode Extensions - AppendChild
-	Updated to work on empty XmlDocument object too


Version 1.0.0.0 (2012-08-28)
===============

String Extensions - IsValidEmailAddress
-	Checks if a string is a valid email address

(Nullable) DateTime Extensions - To_HH_mm_ss
-	Converts to HH:mm:ss string
-	Optional seperator string

(Nullable) DateTime Extensions - To_yyyy_MM_dd
-	Converts to yyyy-MM-dd string
-	Optional seperator string

(Nullable) DateTime Extensions - To_yyyy_MM_dd_HH_mm_ss
-	Converts to yyyy-MM-dd HH:mm:ss string
-	Optional seperator strings

Nullable DateTime Extensions - CompareTo
-	Compares two nullable DateTime objects
-	null is handled as DateTime.MinValue

XmlNode Extensions - AppendChild
-	Adds an XmlElement with a given name to the ChildNodes

XmlNode Extensions - AppendAttribute
-	Adds an XmlAttribute with a given name to the Attributes

XmlNode Extensions - SetAttributeValue
-	Sets the Value of an attribute with a given name
-	The attribute doesn't have to exist

XmlNode Extensions - GetAttributeValue
-	Gets the Value of an attribute with a given name
-	Returns null if the attribute doesn't exist

XmlNode Extensions - GetAttributeDateTimeValue
-	Gets the DateTime Value of an attribute with a given name
-	The value must be in a standard format (yyyy-MM-dd HH:mm:ss or yyyy-MM-dd or HH:mm:ss)
-	Returns null if the attribute doesn't exist

XmlNode Extensions - CopyAttributesFrom
-	Copies all attributes from the source node
-	The source node doesn't have to belong to the same XmlDocument
