### PayByPhone Twitter Task

A simple C# ASP .NET webservice that retrieves the timelines of twitter users, aggregates, sorts and returns them as a JSON response. Requirements can be [found here](https://github.com/katbyte/cs.asp.pbpTwitterTask/blob/master/requirements.txt).

**master** is currently automatically deployed to azure @ [pbptwittertask.azurewebsites.net](http://pbptwittertask.azurewebsites.net/api/aggregate).

endpoints are:

url | use | example
--- | --- | ---
**/api/aggregate/[account1;account2]**| aggregates supplied feeds | *[/api/aggregate/pay_by_phone;PayByPhone;PayByPhone_UK](http://pbptwittertask.azurewebsites.net/api/aggregate/pay_by_phone;PayByPhone;PayByPhone_UK)*
**/api/feed/[account]** | returns a single feed | *[/api/feed/pay_by_phone/](http://pbptwittertask.azurewebsites.net/api/feed/pay_by_phone)*
