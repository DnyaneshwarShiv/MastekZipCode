# MastekZipCode

## Steps for deployment
1. create s3 bucket using cloudformation to store backend output
	* use backendoutputs3.json cloud formation file to create that
	* go to repository path and open deployment folder in file explorer and press cmd in url
	* in command prompt add this command `aws cloudformation deploy --template-file backendoutputs3.json --stack-name backendoutputs3"`	
     * wait till resources created using cloudformation.Once resources gets created we will get success message
2. deploy backend/API changes and create lambda and API gateway
    * go to MastekDeveloperTest folder from file explorer of repository
	* type cmd in url of file explorer which points to MastekDeveloperTest folder
    * run `dotnet lambda deploy-serverless command`
	* Once it ask bucket name add bucket name as `zipcode-backend-output`
	* wait till resources created and print api url. Copy that url we will need it in frontend deployment step 4.
3. create s3 bucket to deploy frontend code
	* go to code repository from file explorer
	* open deployment folder and type cmd in url
	* in command prompt add this command `aws cloudformation deploy --template-file s3Cloudformation.json --stack-name s3Cloudformation`
	* wait till resources created using cloudformation.Once resources gets created we will get success message.
4.  deploy frontend code 
	* go to mastekclient.react folder of repository.
	* edit `.env-development` and change value of `REACT_APP_API_BASEURL` to copied url from e point 2nd step and save it
	* run `deploy-dev.sh`
	* It will build the react code and ask for key and secrete if key and secrete are pesent in credentials file of your system aws folder then just press enter else add your key and secretes there
	* now frontend code will deploy to the your s3 bucket.
	* go to aws now and copy the static website url and paste it in browser
	
We are ready to use the deployed website