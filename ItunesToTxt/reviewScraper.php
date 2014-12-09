<?php
/**
 * Scrape the number of app reviews from iTunes.
 *
 * Set the iOS app id and the number of pages to scrape, and it creates a {$app_id}-reviews.csv file
 *
 * @author Kent Bye <kent@kentbye.com>
 * Modified and extended from Sean Murphy's gist at https://gist.github.com/1878352
 */

// Set the id for the app (Right click on the icon to copy the link in iTunes.
// It should look something like this: http://itunes.apple.com/us/app/netflix/id363590051?mt=8
$app_id = '363590051'; // Netflix app


// Manually set the number of review pages for the app. Select "All Versions" of reviews in iTunes and see what the last number is.
// Default is set to one to grab the latest 10 results
$total_number_of_review_pages = 1;

// Initialize the results array
$results = array();

// Just show US reviews
$countries = json_decode('[{"storefront":"143441-1,12","name":"United States"}]');

// For more countries, uncomment and add more country codes from this list: https://gist.github.com/1878352
// $countries = json_decode('[{"storefront":"143441-1,12","name":"United States"},{"storefront":"143455-6,12","name":"Canada"}]');

// Write the results to a CSV file named after the $app_id
$fp = fopen($app_id . '-reviews.csv', 'w');
    
// Add in column names to the CSV file
$column_names = array('Date', 'Version', 'Rating', 'Review Title', 'Review', 'Helpful Percent', 'Helpful Votes', 'Total Votes', 'Username', 'User Page', 'Review ID', 'Country');
fputcsv($fp, $column_names);


// Start on the first page of most recent results, and continue to the manually set number of pages to crawl
for ($page = 1; $page <= $total_number_of_review_pages; $page++) {
  // Loop through each of the countries
  foreach ($countries as $country) {
    $ch = curl_init();
    // Grab app reviews sorted by most recent and specify the specific page
    curl_setopt($ch, CURLOPT_URL, "https://itunes.apple.com/WebObjects/MZStore.woa/wa/customerReviews?displayable-kind=11&id={$app_id}&page={$page}&sort=4");
    curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);
    curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, 0);
    // Set the user agent to iTunes in order to get the full results
    curl_setopt($ch, CURLOPT_HTTPHEADER, array(
        'User-Agent: iTunes/10.3.1 (Macintosh; Intel Mac OS X 10.6.8) AppleWebKit/533.21.1',
        "X-Apple-Store-Front: {$country->storefront}",
        'X-Apple-Tz: -18000',
        'Accept-Language: en-us, en;q=0.50',
    ));
    $body = curl_exec($ch);
    curl_close($ch);

    $dom = new DOMDocument();
    @$dom->loadHTML($body);


    // Set the XPath selectors for the titles, ratings, username & link, Version + Date, Actual review, and how many people found it helpful
    $xpath = new DOMXPath($dom);
    $review_titles = $xpath->query('//html/body/div[@class="customer-reviews"]/div[5]/div[@class="paginated-container"]/div/div/h5/span');
    $ratings = $xpath->query("/html/body/div[2]/div[5]/div[3]/div/div/h5/div/@aria-label");
    $user_links = $xpath->query("/html/body/div[2]/div[5]/div[3]/div/div/span/a/@href");
    $users_versions_dates = $xpath->query("/html/body/div[2]/div[5]/div[3]/div/div/span");    
    $review_bodies = $xpath->query("/html/body/div[2]/div[5]/div[3]/div/div/p[1]");
    $helpfulness_ratings = $xpath->query("/html/body/div[2]/div[5]/div[3]/div/div/p[2]/span[1]");    
    $review_id_urls = $xpath->query("/html/body/div[2]/div[5]/div[3]/div/div/div/@report-a-concern-fragment-url");


    // DEBUGGING: Output the full html from iTunes to confirm that the data is correct and matches what you expect from iTunes
    // Write the output to an *.html file and confirm it's appearance in a web browser.
    echo $body; 

    // Convert the objects into a results array
    // Review title
    $i = 1;
    foreach ($review_titles as $review_title) {
      $results[$i][3] = $review_title->nodeValue;
      $i++;
    }

    // Star rating
    $i = 1;   
    foreach ($ratings as $rating) {
      // Just return the first character, which is the star rating;
      $results[$i][2] = $rating->nodeValue[0];
      $i++;
    }

    // User, version number and date
    $i = 1;   
    foreach ($users_versions_dates as $user_version_date) {
      $split_user_version_date = explode("\n", $user_version_date->nodeValue);
      $results[$i][8] = trim($split_user_version_date[4]); // User
      $results[$i][1] = trim($split_user_version_date[8]); // Version Number
      $results[$i][0] = trim($split_user_version_date[11]); // Date
      $i++;
    }
    
    // The text of the actual review
    $i = 1;   
    foreach ($review_bodies as $review_body) {
      // Remove white space and character return at beginning 
      $results[$i][4] = trim($review_body->nodeValue);
      $i++;
    }

    // How many people found that the review to be helpful or not helpful
    $i = 1;   
    foreach ($helpfulness_ratings as $helpfulness_rating) {
      // If it starts with "Was this review helpful?" then don't include since their review hasn't been evaluated by other users yet.
      // NOTE: This initial letter of 'W' works for English, but for Spanish, it'd be "E" for Esta
      // TODO: This logic could be improved.
      if ($helpfulness_rating->nodeValue[0] == "W") {
        $results[$i][5] = "";
        $results[$i][6] = "";
        $results[$i][7] = "";
      } else {
        $split_helpfulness_rating = explode("\n", $helpfulness_rating->nodeValue);
        $helpful = explode(" ", $split_helpfulness_rating[0]);
        $total_votes = explode(" ", trim($split_helpfulness_rating[1]));
         $results[$i][5] = intval($helpful[0])/intval($total_votes[0]); // Total Percentage of Helpfulness
        $results[$i][6] = $helpful[0]; // Number of Helpful Votes
        $results[$i][7] = $total_votes[0]; // Number of Total Votes
      }
      $i++;
    }

    // Link to the user's page of reviews
    $i = 1;   
    foreach ($user_links as $user_link) {
      $results[$i][9] = $user_link->nodeValue;
      $i++;
    }

    // Review ID
    $i = 1;   
    foreach ($review_id_urls as $review_id_url) {
      $review_id = explode("=", $review_id_url->nodeValue);
      $results[$i][10] = $review_id[1];

      // Add country name in the last column      
      $results[$i][11] = $country->name;
      $i++;
    }

    // DEBUGGING: Check to see that the information in the array is displayed properly
    print_r($results); 
    
    // Write each of the rows to the CSV file
    foreach ($results as $fields) {
      // Sort the results so that it properly outputs to the CSV file
      ksort($fields);
      fputcsv($fp, $fields);
    }
  }
}

fclose($fp);
