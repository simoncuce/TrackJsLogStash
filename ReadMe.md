# Track JS LogStash

This tool allows to Track JS errors to be copied to Elastic Search. It copies all errors from the last sync date time. 

## Installation

There is no installer, you will need to download this code and compile it.

## Setup

To Setup the tool you need the following mandatory information

* Track JS Customer ID (details found at http://docs.trackjs.com/data-api/index)
* Track JS API Key (details found at http://docs.trackjs.com/data-api/index)
* URL to Elastic Search endpoint

The is also an optional item 

* Track JS Application Name (details can be found at http://docs.trackjs.com/data-api/errors)


You need to update the Application Config

```
 <appSettings>
    <add key="TrackJsClientId"  value="<CustomerId>"/>
    <add key="TrackJsApiKey"  value="<ApiKey>"/>
    <add key="TrackJsApplication"  value="<Application>"/>
  </appSettings>
```

```
 <nlog>
    <extensions>
      <add assembly="NLog.Targets.ElasticSearch" />
    </extensions>

    <targets async="true">
      <target name="elastic" type="BufferingWrapper" flushTimeout="5000">
        <target type="ElasticSearch" uri="<Elastic Search URL>" >
          <field name="message" layout="${event-properties:item=messageDetails}" />
          <field name="timestamp" layout="${event-properties:item=timestamp}" />
          <field name="id" layout="${event-properties:item=messageId}" />
          <field name="browserName" layout="${event-properties:item=browserName}"  />
          <field name="broswerVersion" layout="${event-properties:item=broswerVersion}"  />
          <field name="entry" layout="${event-properties:item=entry}"  />
          <field name="line" layout="${event-properties:item=line}"  />
          <field name="column" layout="${event-properties:item=column}"  />
          <field name="file" layout="${event-properties:item=file}"  />
          <field name="userId" layout="${event-properties:item=userId}"  />
          <field name="sessionId" layout="${event-properties:item=sessionId}"  />
          <field name="trackJsUrl" layout="{$event-properties:item=trackJsUrl}"  />
          <field name="metadata" layout="{$event-properties:item=metadata}"  />
        </target>
      </target>
    </targets>
    <rules>
      <logger name="*" minlevel="Info" writeTo="elastic" />
    </rules>
  </nlog>
```


## References

This tool uses the TrackJs API. Details can be found at (http://docs.trackjs.com/data-api/errors)

The inserting into Elastic Search is done via  NLog and the NLog.Targets.ElasticSearch (https://github.com/ReactiveMarkets/NLog.Targets.ElasticSearch)