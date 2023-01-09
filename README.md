# GameReplayer

The purpose of this app is to read the actions created from json file stored in S3 buckets and fire them.

The actions list first need to sorted according to the timestamp in ascending order. Then each action in the list need to be fired. The subsequent actions need to be fired at the same duration after the previous action. This would make it look like someone is trading a game.

**Assumption:** <br />
Rest Api details would be stored in S3 bucket files. Sample file is in Application/Dummy.json

**Config file change** <br />
Use Helpers/GameHelper.cs to update fields accordingly <br />
BaseAddress: URL of the model <br />
Cookie: For Authentication/Authorization <br />


