using Microsoft.AspNetCore.Mvc;
using Reddit;
using Reddit.Controllers;

namespace RedditStatsAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class RedditController : ControllerBase
  {
    private readonly RedditClient _reddit;
    private readonly Dictionary<string, int> _userPostCounts;
    private Post _mostUpvotedPost;
    string token = "";
    string AppID = "";
    string AppSecret = "";

    public RedditController()
    {
      // Initialize Reddit client
      _reddit = new RedditClient(appId: AppID, appSecret: AppSecret, accessToken: token);
      
      _userPostCounts = new Dictionary<string, int>();
    }

    [HttpGet("fetchposts/{subreddit}")]
    public async Task<IActionResult> FetchPosts(string subreddit)
    {
      try
      {
        // Fetch posts from the subreddit asynchronously
        var subredditObject = _reddit.Subreddit(subreddit);
        var posts = await Task.FromResult(subredditObject.Posts.GetNew().Take(1000).ToList());

        // Process the posts to track statistics
        foreach (var post in posts)
        {
          // Update most upvoted post
          if (_mostUpvotedPost == null || post.Score > _mostUpvotedPost.Score)
          {
            _mostUpvotedPost = post;
          }

          // Update user post count
          if (!_userPostCounts.ContainsKey(post.Author))
          {
            _userPostCounts[post.Author] = 1;
          }
          else
          {
            _userPostCounts[post.Author]++;
          }
        }

        // Return statistics along with success message
        var statistics = new
        {
          MostUpvotedPost = _mostUpvotedPost?.Title,
          UsersWithMostPosts = _userPostCounts.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).ToList()
        };

        return Ok(statistics);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred: {ex.Message}");
      }
    }
  }
}