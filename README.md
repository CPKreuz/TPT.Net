# TPT.Net
TPT.Net is a simple API wrapper for the physics sandbox game The Powder Toy.

You can get information on a User or Save and you can search for Saves

Logging in and changing a Save or User is planned for the future

# Basic usage

```cs
TPTClient client = new TPTClient();
SaveInfo save = await client.GetSaveInfoAsync(2617661);
UserInfo user = await client.GetUserAsync("CPK");
BrowseResult frontPage = await client.GetFrontPage();
BrowseResult cpksSaves = await user.GetSaves();
```
