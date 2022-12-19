# Projektuppgift Systemintegration

## how to build the project

- `cd UrlShortenerAPI`
- - `docker build . -t urlshortenerapi`
- - `cd ../`
- `cd UrlRedirectService`
- - `docker build . -t urlredirectservice`
- - `cd ../`
- `cd LoginService`
- - `docker build . -t loginservice`
- - `cd ../`
- `docker-compose -f deploy.yml up`

## How to test the microservices:

### step 1 - Register

- Send a post request to `http://localhost:5229/register` containing a similar body:

  ```json
  {
    "name": "Per Person",
    "email": "per.person@email.com",
    "password": "Test123!",
    "role": "Administrator"
  }
  ```

### step 2 - Login

- Get a JWT token by sending a post request to `http://localhost:5229/login` containing a similar body:

  ```json
  {
    "email": "per.person@email.com",
    "password": "Test123!"
  }
  ```

### step 3 - Use the URL Shortener

- Use the JWT token and make a post request to `http://localhost:5227/shorten` specify the url you want to shorten in the body.

  ```json
  {
    "url": "https://www.berrykitten.com"
  }
  ```

### step 4 - Use the shortened URL

- Paste the response from **step 3** in your browser. If you have followed the steps correctly you should be redirected to the URL you entered in **step 3**.
