pipeline {
    agent any
    parameters {
        choice(
            choices: ['Withdbchanges' , 'Withoutdbchanges'],
            description: '',
            name: 'REQUESTED_ACTION')
    }
    
    stages {
        stage('SCM Checkout') {
            steps {
                git 'https://github.com/pnewalkar/Sync.git'
            }
        }
        stage('Dotnet Build') {
            steps {
                sh label: '', script: '''cd Maintel.Icon.Portal.Sync.HighlightAPI
				dotnet build'''
            }
        }
		stage('Dotnet Test') {
            steps {
                sh label: '', script: '''cd Maintel.Icon.Portal.Sync.HighlightAPI.Spec
				dotnet test --settings:Settings/dev.runsettings'''
            }
        }
		stage('Zip Artifacts') {
            steps {
                sh label: '', script: '''cp /var/lib/jenkins/workspace/demo-jenkinsfile-git/Maintel.Icon.Portal.Sync.HighlightAPI/bin/Debug/netcoreapp2.2/* /Artifacts/
				zip -r Artifacts.zip /Artifacts'''
            }
        }
        stage('Code Deployment') {
            steps {
                sh label: '', script: 'ansible-playbook /opt/deployment/sync-deployment.yml -i /opt/deployment/hosts'
            }
        }
        stage ('db deployment') {
            when {
                // Only perform db deployment if a "Withdbchange" is requested
                expression { params.REQUESTED_ACTION == 'Withdbchanges' }
            }
            steps {
                //echo "Hello, parag!"
                sh label: '', script: 'ansible-playbook /opt/deployment/db-deployment.yml -i /opt/deployment/hosts'
            }
        }
    }
}
