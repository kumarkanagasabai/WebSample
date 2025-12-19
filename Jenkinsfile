pipeline {
    agent any

    options {
        timestamps()
        skipDefaultCheckout()
    }

    environment {
        BUILD_LOCATION = 'pr-merge-block'
    }

    stages {

        /* =====================
           CHECKOUT
        ====================== */
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        /* =====================
           PRE-MERGE (PR)
        ====================== */
        stage('Pre-Merge Validation') {
            when {
                expression { env.CHANGE_ID != null }
            }
            steps {
                script {

                    def compileSuccess = true
                    def failureReason = ''

                    try {
                        sh '''
                          dotnet restore
                          dotnet build
                        '''
                    } catch (e) {
                        compileSuccess = false
                        failureReason = 'Source Compilation Failed'
                        currentBuild.result = 'FAILURE'
                    }

                    // def message = buildPreMergeMessage(
                    //     compileSuccess,
                    //     failureReason,
                    //     env.BUILD_LOCATION
                    // )

                    // postPrComment(message)

                    if (!compileSuccess) {
                        error('Pre-merge validation failed')
                    }
                }
            }
        }

        /* =====================
           POST-MERGE (MAIN)
        ====================== */
        stage('Post-Merge Build') {
            when {
                allOf {
                    branch 'main'
                    expression { env.CHANGE_ID == null }
                }
            }
            steps {
                sh '''
                  dotnet restore
                  dotnet build -c Release
                  dotnet publish -c Release
                '''
            }
        }
    }
}