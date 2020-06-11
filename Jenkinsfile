node{
   stage('SCM Checkout'){
     git 'https://github.com/pnewalkar/Sync.git'
   }
   stage('Dotnet Build'){
      sh label: '', script: '''cd Maintel.Icon.Portal.Sync.HighlightAPI
      dotnet build'''
   }
    stage('Dotnet Test'){
      sh label: '', script: '''cd Maintel.Icon.Portal.Sync.HighlightAPI.Spec
      dotnet test'''   
   }
}
