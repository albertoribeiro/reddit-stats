# reddit-stats

RedditStatsAPI is a C# .NET Core Web API application that consumes posts from a specified subreddit, tracks statistics such as posts with the most upvotes and users with the most posts, and provides an endpoint to retrieve these statistics.

## Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download) installed on your machine.

## Getting Started

1. Clone this repository to your local machine:

```bash
git clone <repository-url>
```

2. Navigate to the project directory:
```bash
cd RedditStatsAPI 
```

## Running the Application

1. Clone this repository to your local machine:
 

```bash
dotnet run
```

2. Once the application is running, you can test the functionality by making HTTP GET requests to the following endpoint:
GET /reddit/fetchposts/{subreddit}: Fetch posts from the specified subreddit.
Replace {subreddit} with the name of the subreddit you want to fetch posts from.

### Example Usage

```bash
curl -X GET https://localhost:5001/reddit/fetchposts/programming
```