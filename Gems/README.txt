Graphical Administration Tool for TIBCO(R) EMS
==============================================

Graphical Administration Tool for TIBCO(R) EMS (GEMS) is a graphical user interface utility for TIBCO(R) Enterprise Message Service. It can be used by JMS developers as a general purpose testing tool and by administrative support staff as a management and monitoring tool.

Author: Richard Lawrence (rlawrenc@tibco.com)


Requirements:
------------

- TIBCO EMS version 5.0 or later
- JRE 1.6 or later (1.7 for EMS8 and above)
- JFreeChart jcommon-1.0.23.jar and jfreechart-1.0.19.jar libraries available from www.jfree.org/jfreechart

Version 5.2
-----------
- Added support for new EMS 8.5 server properties:
	- getHealthCheckListen
	- getSecondaryHealthCheckListen
- Added ability to specify servers.xml from command line
- Added script to build docker image: build-docker-image.sh
- Added page next on Queue Browser
- Added ability to set following properties to JSON Editor:
	- always_exit_on disk_error
	- health_check_listen
	- secondary_health_check_listen
- Added Local and XA Transaction Monitors to Server menu
- Added In/OutByteRate, In/OutTotalBytes, AvgMsgSize to Queue and Topic Info display
- Added ephemeral client port to Connection Info display
- Added ability to set default store size when creating new file store in JSON Editor
- Added event type combo box to Queue/Topic Monitors

Version 5.1
-----------
- Added support for new EMS 8.4 server properties:
	- get/setLargeDestCount
	- get/setLargeDestSize
	- getAdminConnectionCount
	- getClientConnectionCount
	- isAlwaysExitOnDiskErrorEnabled
	- isSSLRequireRouteCertOnly
- Destination PendingMsgCount/Size fields are highlighted in red if they reach MaxMsgs/Bytes or Server’s large destination limits (whichever is the smaller).
- Added ability to send previously saved messages, available from Queues and Topics menu.
- Added right click menu on tree view with option for connecting/disconnecting all servers in folder.
- Client side trace can now be enabled on a client connection from the Server menu.
- Now prompts to save config changes on close.
- Server Info panel now highlights if EMS server has config changes that require restart.
- Stores can now be sorted e.g. by file size etc.
- JSON Editor validation now checks for transport bridge duplicates.
- Copying browsed queue messages or all queue messages now preserve message expiration.
- Added MaxMonitorEvents property in gems.props:
	- Limits the number of message events for Destination monitor and browser dialogs.

Note: EMS 8.4 no longer ships with tibcrypt.jar; any previously saved GEMS servers.xml file with encrypted passwords will not be decrypted with EMS 8.4 client.
To migrate GEMS encrypted passwords first run GEMS 5.1 with EMS 8.3 client and resave the GEMS config to servers.xml, then 
upgrade to EMS 8.4 client, restart GEMS with the new servers.xml and passwords will be decrypted correctly.


Version 5.0
-----------
- Added Todays and Historical Charts to view stats from Server Info CSV log files.
- Added ability to copy a queue's messages to another destination (or server), select "Copy All Messages.." from Queues menu.
- Server Info logging is now enabled by default
- Added icons to tree view
- Added Queue and Topic store filter on Connection tab or servers.xml
- Added ability to filter displayed queues, topics for a specified store:
	- A queueStoreFilter/topicStoreFilter can be set (in servers.xml or on Connection tab) only destinations that are assigned to the store are displayed.
- Queues display now sorts by PendingMsgCount by default
- Queues and Topics sub-trees now hidden (can be overriden using HideTreeDests property in gems.props)
- Message import now copies JMSType and JMSCorrelationID
- Server totals are now shown by default in the Server Monitor display.
- Server Warning and Error limits are now enabled by default.
- Added "New Listen Port" option to Create menu in JSON Config Editor.

Version 4.5
-----------
- Added support to set server properties in JSON Config Editor.
- Added support to import LDAP properties in JSON Config Editor.
- Added ConnectionId to Temporaries view in Queues and Topics display
- Added LDAP parameters to Server Info tab.
- Added topic prefetch property to Route Info display.
- Added AllowMsgReadInViewOnlyMode property in gems.props to allow/disallow message browsing in read only mode.
- Added AutoConnectOnStart property in gems.props, when false delays the auto connect until after the main display is shown
- Added support for TIBCO Substation ES-2.9.
- Improvements for handling timeout exceptions waiting for server response. 

Version 4.4
-----------
- Added additional support to JSON Config Editor for:
	- Importing ACLs
	- Importing Routes
	- Creating file stores
	- Creating connection factories
	- Validation function to check for duplicates
- Added ability to set destination permissions from popup Queue/Topic displays.
- Column headers are now printed to server statistics log file on restart.
- The property setting AutoReconnectToPrimary now defaults to true.

Version 4.3
-----------
- Added support for EMS8.2:
	- FTLParams shown on Server Info display
	- FTL transports shown on Transports display
- Added "Update Connection Factory" to Factories menu.
- Principal lists in destination and admin permissions dialogs are now sorted.

Version 4.2
-----------
- Added ability to filter displayed queues, topics and clients for each EMS Server.
	- A queueNamePattern/topicNamePattern can be set (in servers.xml or on Connection tab) only destinations that match the pattern are displayed.
	- A userNameFilter can also be set to limit client displays for given user name.
- Added support for EMS Appliance V2.1.

Version 4.1
-----------
- Added FaultTolerantState to main EMS Server display (see "Monitoring Fault Tolerant Servers" section in HTML docs).
- Added Connections(User) display, shows connection statistics per user.
- Added support for monitoring TIBCO EMS Appliance (see servers.xml file and "Monitoring TIBCO EMS Appliance" section in HTML docs).
- Added Server JSON Configuration Editor to support migration to EMS Appliance (see "Migrating to TIBCO EMS Appliance" section in HTML docs).
- Added support for monitoring by application when several applications share the same EMS server:
	- When ViewOnlyMode and QueueNamePattern/TopicNamePattern are set (in gems.props) only destinations that match the pattern are displayed.
	- Ability to filter display for given user name, see UserNameFilter in gems.props.
- Added runsimplegems.cmd and simplegems.props examples to run GEMS as a view only monitoring tool.
- Added support for TIBCO Substation ES-2.8.
- Added ability to display Substation output from CICS commands SXTH/Transactions.


Version 4.0
-----------
- Added support for EMS8 and JMS2.0
	- New Topic Subscriptions Display
	- Support for message JMSDeliveryTime
	- Messages awaiting their delivery time are held in a delayed queue ($sys.delayed.q.<queue name> or $sys.delayed.t.<topic name>)
	- Messages held in delayed queues may be browsed and managed using the Queue Browser.
- All displays that list destinations now use cursor based calls to reduce server impact when there are many destinations.
	- Default cursor size may be configured in gems.props.
- JRE1.5 is no longer supported.
- To access EMS8 & JMS2.0 features you must use JRE1.7 and EMS8 client jars, including new jms-2.0.jar which replaces jms.jar

Version 3.5
-----------
- Added new Edit menu options:
	- Add New EMS Server
	- Remove Selected EMS Server
	- Add New Folder
	- Remove Selected Folder
	- Edit Warning Limts
	- Edit Error Limits
	- Edit Monitor Events
- Added support for new EMS 6.1 features: destination persistent message size/count, data store read/write stats.
- Added "Save Config" option to File menu to save configuration with support for password encryption.
- Added Show queue/topic consumers display.
- Added EMS server response time (millisecs) to the Server Monitor display, charting and logging.
- Added ability to configure maximum number of consumers/producers displayed in main display, see MaxConsumers, MaxProducers in gems.props.
- Added ability to copy and destroy selected messages in Durable Browser.
- Added JNDI lookup option from Server menu.
- Improved destination monitor display, with latency statistics.
- Added support for TIBCO Substation ES-2.7 (Message Format changes).
- Added ability to invoke SubStation Recipe RECOVER command.

Note: Support for EMS 4.X has been deprecated.

Version 3.4
-----------
- Added show queues/topics display
- Added queue/topic properties dialog
- Improved charting display and new features including; Properties Editor, Print, Copy and Save As menu options
- New Request/Reply Tester 
- HTML Help Display (JRE 1.6 only)
- Improved Transports and Transactions display
- Added ability to configure maximum number of queues/topics displayed in main display, see MaxQueues, MaxTopics in gems.props
- Added ability to configure main server display column positions, see ServerInfoColPositions property in gems.props
- Added ability to configure delimiter used as separator between values in CSV file outputs, see CSVFileDelimiter property in gems.props


Version 3.3
-----------
- New monitor connections dialog available in Server menu
- New monitor routes dialog available in Routes menu
- Add option in message destroyer dialog to use Admin API or JMS read operation
- Add support for SubStation IMS statistics, and improve multi-interface display
- Add LogDateTimeFormat property in gems.props
- Ignore temporary destinations in destination picker for improved performance
- Support for message property values containing ':' in message dialog save/load operations

Version 3.2
-----------

New Features:

- Support for new EMS6.0 features:
	- MStore Info display
	- Fragmentation level on File Store display
	- Server Info properties: MaxClientMsgSize, DestinationBacklogSwapout
	- Queue Info property: RedeliveryDelay
- New Look & Feel (customizable in gems.props)
- Manage Bridges Dialog
- Server info logging (see servers.xml)
- Support for MapMessages
- Configure Server Trace Dialog
- Save messages to file from Browser/Subscriber File menu
- Set Request/Reply timeout in Request/Reply Monitor Edit Menu Options Dialog
- Filtering of TextMessages in Queue Browser via Edit Menu
- Improved SubStation support for V2.5 (see servers.xml for details)
- Column size customization for details panel available with DetailPanelColWidths property


Version 3.1
-----------

New Features:

- Charting of server info
- JMS Request/reply monitoring (Queue/Topic menu)
- Server Event monitoring (see servers.xml)
- Management and monitoring support for TIBCO SubStation (see servers.xml)
- Configurable tree view (see hide views option in gems.props)
- Totals displayed in server monitor, view via right mouse button menu
- Create/Destroy Connection Factories menu entries
- Improved support for FT connections (see servers.xml)
- Administration changes to backup servers are now disabled by default (see gems.props)

Version 3.0
-----------

New Features:

- Support for EMS5.0:
	- Channels Info panel
	- Stores(File), Stores(DB) Info panel
	- Server Info: isMulticastEnabled, MulticastStatisticsInterval, StoresFile, ChannelsFile
	- Queue Info: Store, isStoreInherited
	- Topic Info: Store, isStoreInherited, Channel, isChannelInherited, isMulticastEnabled
	- Connection Info: UncommittedCount, UncommittedSize
	- Consumer Info: Multicast
	- Route Info: BacklogCount, BacklogSize
- Copy selected messages in Queue Browser window to another destination/server
- Destroy selected messages in Queue Browser window
- Purge multiple queues/topics
- Service (request/reply) statistics collection (see servers.xml)
- View Text messages as XML via right mouse click popup menu
- View Bytes messages as text via right mouse click popup menu
- Custom column widths are maintained between displays
- Save tabular data to CSV file via right mouse click popup menu
- View original message from $sys.monitor messages
- New configurable properties (see gems.props):
	MaxDisplayBytes - Maximum display size for bytes messages

Version 2.4
-----------

New Features:

- Support for new EMS4.4 properties:
	- PendingMsgCount, PendingMsgSize and selector on consumer properties
	- Prefetch, MaxMsgs and OverflowPolicy on topic properties
	- isRouted and getRouteName queue properties
- SSL connectivity (see servers.xml)
- Cut and Paste on edit menu and right mouse click popup menu
- Selector wizard for queue browsing by message timestamp
- Options editor in Queue Browser, Durable Browser and Topic Subscriber
- Column sorting on Server Info and Message Browser panels
- Route Management and highlighting for disconnected routes.
- Routing info added to Queues Info display
- PermType setting in gems.props to show destinations by permanence type (default; no temps)
- Tree icons and custom icons configurable in servers.xml

Version 2.3
-----------

New Features:

- Support for new EMS4.3 properties
- Destination and Administration permissions management
- Bridge management
- Queue and Topic Monitor
- View only mode
- Improved bridge and route details
- AutoConnect attribute in servers.xml
- Customisable tree view in servers.xml e.g. to group servers in regions or by environment
- Pending message sizes highlighted in red when reaches 80% of MaxBytes or MaxMsgs limit

Version 2.2
-----------

New Features:

- Storage Info display tab
- Set server properties dialog
- Set destination properties dialog

Version 2.1
-----------

New Features:

- Main Server Monitor display with configurable limit minding and colour highlighting
- User management; create, update, destroy user
- Durable management; create, destroy, purge, browse pending messages
- Automatic display refresh
- Column sorting on display panel
- Save/load text messages to/from file
- Loads server connection details and limits from servers.xml file
- Display properties configuration file (gems.props)
- Destroy a client's EMS connection


Version 1.0
-----------

Supports:
 
- Multiple EMS server connections
- Displays configuration and statistics for topics, queues, ACLs, routes, connections, durables, groups, producers, transactions, transports, users.
- Queue management; create, destroy, purge, browse messages, send text messages
- Topic management; create, destroy, purge, subscribe to messages, publish text messages


Support:
--------

THIS TOOL IS NOT AN OFFICIAL PRODUCT OF TIBCO.
TIBCO IS NOT RESPONSIBLE FOR ANY LOSS OR ISSUE RESULTING FROM THE USE OF THIS TOOL.

Only the author may provide non-immediate or non-complete support.

Please, send any requests, comments, suggestions, issues to the author:

Richard Lawrence (rlawrenc@tibco.com)
